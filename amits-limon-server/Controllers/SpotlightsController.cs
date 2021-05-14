using amits_limon_server.Filters;
using amits_limon_server.Interfaces;
using amits_limon_server.Models;
using amits_limon_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    public class SpotlightsController : ControllerBase
    {
        readonly private IGetSpotlights _spotlightService;
        readonly private ILogger<SpotlightsController> _logger;
        private readonly IAdminOperations _adminService;
        private readonly ObjectValuesChecker _checker;

        public SpotlightsController(IGetSpotlights spotlightService, ILogger<SpotlightsController> logger, IAdminOperations adminService, ObjectValuesChecker checker)
        {
            _spotlightService = spotlightService;
            _logger = logger;
            _adminService = adminService;
            _checker = checker;
        }
        [HttpGet]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public ActionResult<IEnumerable<Spotlight>> Get()
        {
            return InnerGet();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Spotlight>>> Delete(int id)
        {
            try
            {
                Spotlight spotlight = _spotlightService.GetSpotlights().FirstOrDefault(spotlight => spotlight.SpotlightId == id);
                await _adminService.Delete(spotlight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while deleting spotlight");
                return Problem("exeption while getting spotlight");
            }
            return InnerGet();
        }

        [Authorize]
        [HttpPost]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Spotlight>>> Post([FromBody] Spotlight spotlight)
        {
            try
            {
                if (_checker.IsObjectComplete(spotlight, true))
                    await _adminService.Add(spotlight);
                else
                {
                    _logger.LogError("invalid spotlight tried to be added");
                    return Problem("invalid spotlight tried to be added");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while adding spotlight");
                return Problem("exeption while adding spotlight");
            }

            return InnerGet();

        }

        [Authorize]
        [HttpPut]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Spotlight>>> Put(Spotlight spotlight)
        {
            try
            {
                if (_checker.IsObjectComplete(spotlight))
                    await _adminService.Update(spotlight);
                else
                {
                    _logger.LogError("invalid spotlight tried to be updated");
                    return Problem("invalid spotlight tried to be updated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while updated spotlight");
                return Problem("exeption while updated spotlight");
            }
            return InnerGet();

        }

        private ActionResult<IEnumerable<Spotlight>> InnerGet()
        {
            try
            {
                return Ok(_spotlightService.GetSpotlights().ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while getting spotlights");
                return Problem("exeption while getting spotlights");
            }
        }


    }
}
