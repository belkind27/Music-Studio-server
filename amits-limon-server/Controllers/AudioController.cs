using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace amits_limon_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AudioController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public AudioController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] string FileName)
        {
            //send origin header by myself because cors policy not inforced in media 
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");

            //send ranges header to allow the client to control the media
            Response.Headers.Add("Accept-Ranges", "bytes");

            return PhysicalFile(Path.Combine(_env.ContentRootPath, "Audio", FileName), "audio/mpeg");
        }
    }
}
