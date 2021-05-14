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
using System.Reflection;
using System.Threading.Tasks;

namespace amits_limon_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CommentsController : ControllerBase
    {
        readonly private IAddComments _commentService;
        readonly private ILogger<CommentsController> _logger;
        readonly private IAdminOperations _adminService;
        readonly private ObjectValuesChecker _checker;


        public CommentsController(IAddComments commentService, ILogger<CommentsController> logger, IAdminOperations adminService, ObjectValuesChecker checker)
        {
            _commentService = commentService;
            _logger = logger;
            _adminService = adminService;
            _checker = checker;

        }
        [HttpPost]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public ActionResult Post(Comment comment)
        {
            Comment[] res = null;
            try
            {
                if (_checker.IsObjectComplete(comment, true))
                    res = _commentService.AddComment(comment).Result.ToArray();
                else
                {
                    _logger.LogError("invalid comment tried to be added");
                    return Problem("invalid comment tried to be added");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while posting comment");
                return Problem("exeption while posting comment");
            }
            return Ok(res);
        }

        [HttpGet]
        [TypeFilter(typeof(ApiSimpleAuthActionFilter))]
        public ActionResult<IEnumerable<Comment>> Get()
        {
            return InnerGet();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> Delete(int id)
        {
            try
            {
                Comment comment = _commentService.GetComments().FirstOrDefault(comment => comment.CommentId == id);
                await _adminService.Delete(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while deleting song");
                return Problem("exeption while getting song");
            }
            return InnerGet();
        }
        private ActionResult<IEnumerable<Comment>> InnerGet()
        {
            try
            {
                return Ok(_commentService.GetComments().ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exeption while getting comments");
                return Problem("exeption while getting comments");
            }
        }
    }
}
