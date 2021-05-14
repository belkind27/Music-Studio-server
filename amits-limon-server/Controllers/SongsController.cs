using amits_limon_server.Filters;
using amits_limon_server.Interfaces;
using amits_limon_server.Models;
using amits_limon_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace amits_limon_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SongsController : ControllerBase
    {
        readonly private IGetSongs _songService;
        readonly private ILogger<SongsController> _logger;
        private readonly IAdminOperations _adminService;
        private readonly ObjectValuesChecker _checker;
        private readonly IWebHostEnvironment _envoirment;

        public SongsController(IGetSongs songService, ILogger<SongsController> logger, IAdminOperations adminService, ObjectValuesChecker checker, IWebHostEnvironment envoirment)
        {
            _songService = songService;
            _logger = logger;
            _adminService = adminService;
            _checker = checker;
            _envoirment = envoirment;
        }

        [HttpGet]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public ActionResult<IEnumerable<Song>> Get()
        {
            return InnerGet();
        }

        [Authorize]
        [HttpPost, DisableRequestSizeLimit]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Song>>> Post()
        {
            Song song = new Song();
            try { song = await ConvertForm(Request.Form); }
            catch (Exception)
            {
                return Problem("exeption while  uploading song");
            }
            try
            {
                if (_checker.IsObjectComplete(song, true))
                    await _adminService.Add(song);
                else
                {
                    _logger.LogError("invalid song tried to be added");
                    return Problem("invalid song tried to be added");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while adding song");
                return Problem("exeption while adding song");
            }
            return InnerGet();

        }

        [Authorize]
        [HttpPut]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Song>>> Put()
        {
            Song song = new Song();
            try { song = await ConvertForm(Request.Form); }
            catch (Exception)
            {
                return Problem("exeption while  uploading song");
            }
            try
            {
                if (_checker.IsObjectComplete(song))
                    await _adminService.Update(song);
                else
                {
                    _logger.LogError("invalid note tried to be updated");
                    return Problem("invalid note tried to be updated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while updated note");
                return Problem("exeption while updated note");
            }
            return InnerGet();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Song>>> Delete(int id)
        {
            try
            {
                Song song = _songService.GetSong(id);
                await _adminService.Delete(song);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while deleting song");
                return Problem("exeption while getting song");
            }
            return InnerGet();
        }
        private ActionResult<IEnumerable<Song>> InnerGet()
        {
            try
            {
                return Ok(_songService.GetSongs().ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while getting songs");
                return Problem("exeption while getting songs");
            }
        }

        private async Task<string> UploadFile(IFormFile file, bool isSong)
        {
            string folder = "Images";
            if (isSong) folder = "Audio";
            string fileName = Guid.NewGuid().ToString()+"_"+ file.FileName;
            string folderName = Path.Combine(_envoirment.ContentRootPath, folder);
            string uploadPath = Path.Combine(folderName, fileName);
            try
            {
                using (FileStream fileStream = new FileStream(uploadPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while uploading file");
                string defaultImg = "amit.jpg";
                if (isSong) throw new Exception();
                else return $"https://localhost:5001/api/{folder}/{defaultImg}";
            }
            if (isSong) return $"https://localhost:5001/api/{folder}?FileName={fileName}";
            else return $"https://localhost:5001/api/{folder}/{fileName}";
        }

        private async Task<Song> ConvertForm(IFormCollection form)
        {
            var upload = new Song();
            try
            {
                foreach (var file in form.Files)
                {
                    if (file.Name == "image")
                    {
                        upload.ImgSource = await UploadFile(form.Files[0], false);
                        if (form.TryGetValue("imgSource", out var imgSource))
                        {
                            DeleteFile(imgSource,false);
                        }
                    }
                    if (file.Name == "audio")
                    {
                        upload.AudioSource = await UploadFile(form.Files[1], true);
                        if (form.TryGetValue("audioSource", out var audioSource))
                        {
                            DeleteFile(audioSource,true);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while uploading audio");
                throw new Exception();
            }
            form.TryGetValue("songId", out var val);
            upload.SongId = int.Parse(val);
            form.TryGetValue("name", out val);
            upload.Name = val;
            form.TryGetValue("description", out val);
            upload.Description = val;
            form.TryGetValue("date", out val);
            upload.Date = DateTime.Parse(val);
            return upload;
        }

        private void DeleteFile(string source, bool isSong)
        {
            string folder = "Images";
            if (isSong) folder = "Audio";
            string folderName = Path.Combine(_envoirment.ContentRootPath, folder);
            string fileName = source.Substring("https://localhost:5001/api/Images//".Length - 1);
            if (isSong) fileName = source.Substring("https://localhost:5001/api/Audio?FileName=".Length);
            string path = Path.Combine(folderName, fileName);

            System.IO.File.Delete(path);
        }
    }
}

