using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace amits_limon_server.Filters
{
    public class ApiSimpleAuthActionFilter : ActionFilterAttribute
    {
        private const string _key = "amitsLimon";
        readonly private ILogger<ApiSimpleAuthActionFilter> _logger;

        public ApiSimpleAuthActionFilter(ILogger<ApiSimpleAuthActionFilter> logger) : base()
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            CheckUserAgent(context);
            CheckApiKey(context);
        }

        private void CheckApiKey(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Api-Key", out var key))
            {
                if (_key != key.ToString())
                {
                    _logger.LogWarning("an attampet to connect to the server with wrong apiKey was identified");
                    ReqNotValid(context);

                }
            }
            else
            {
                _logger.LogWarning("an attampet to connect to the server without apiKey was identified");
                ReqNotValid(context);
            }
        }

        private async void ReqNotValid(ActionExecutingContext context)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "error" }));
            await context.Result.ExecuteResultAsync(context);
        }

        private void CheckUserAgent(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("User-Agent", out var userAgent))
            {
                if (!userAgent.ToString().ToLower().Contains("mozilla")) 
                {
                    ReqNotValid(context);
                }
            }
            else
            {
                _logger.LogWarning("an attampet to connect to the server from unautherized user agent");
                ReqNotValid(context);
            }
        }
    }
}
