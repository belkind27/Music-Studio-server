using amits_limon_server.Filters;
using amits_limon_server.Interfaces;
using amits_limon_server.Models;
using amits_limon_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amits_limon_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        readonly private IGetNotes _noteService;
        readonly private ILogger<NotesController> _logger;
        readonly private IAdminOperations _adminService;
        readonly private ObjectValuesChecker _checker;

        public NotesController(IGetNotes noteService, ILogger<NotesController> logger, IAdminOperations adminService, ObjectValuesChecker checker)
        {
            _noteService = noteService;
            _logger = logger;
            _adminService = adminService;
            _checker = checker;
        }
        [HttpGet]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public ActionResult<IEnumerable<Note>> Get()
        {
            return InnerGet();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Note>>> Delete(int id)
        {
            try
            {
                Note note = _noteService.GetNotes().FirstOrDefault(note => note.NoteId == id);
                await _adminService.Delete(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while deleting notes");
                return Problem("exeption while getting notes");
            }
            return InnerGet();
        }

        [Authorize]
        [HttpPost]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Note>>> Post(Note note)
        {
            try
            {
                if (_checker.IsObjectComplete(note, true))
                    await _adminService.Add(note);
                else
                {
                    _logger.LogError("invalid note tried to be added");
                    return Problem("invalid note tried to be added");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while adding note");
                return Problem("exeption while adding note");
            }

            return InnerGet();

        }

        [Authorize]
        [HttpPut]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Note>>> Put(Note note)
        {
            try
            {
                if (_checker.IsObjectComplete(note))
                    await _adminService.Update(note);
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

        private ActionResult<IEnumerable<Note>> InnerGet()
        {
            try
            {
                return Ok(_noteService.GetNotes().ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while getting notes");
                return Problem("exeption while getting notes");
            }
        }
    }
}
