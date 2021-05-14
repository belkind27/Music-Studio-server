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
    public class RecommendationsController : ControllerBase
    {
        readonly private IGetRecommendations _recommendationService;
        readonly private ILogger<RecommendationsController> _logger;
        private readonly IAdminOperations _adminService;
        private readonly ObjectValuesChecker _checker;


        public RecommendationsController(IGetRecommendations recommendationService, ILogger<RecommendationsController> logger, IAdminOperations adminService, ObjectValuesChecker checker)
        {
            _recommendationService = recommendationService;
            _logger = logger;
            _adminService = adminService;
            _checker = checker;
        }
        [HttpGet]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public ActionResult<IEnumerable<Recommendation>> Get()
        {
            return InnerGet();
        }

        [Authorize]
        [HttpPut]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Recommendation>>> Put(Recommendation recommendation)
        {
            try
            {
                if (_checker.IsObjectComplete(recommendation))
                    await _adminService.Update(recommendation);
                else
                {
                    _logger.LogError("invalid recommendation tried to be updated");
                    return Problem("invalid recommendation tried to be updated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while updated recommendation");
                return Problem("exeption while updated recommendation");
            }
            return InnerGet();
        }
        private ActionResult<IEnumerable<Recommendation>> InnerGet()
        {
            try
            {
                return Ok(_recommendationService.GetRecommendations().ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while getting Recommendations");
                return Problem("exeption while getting Recommendations");
            }
        }
    }
}