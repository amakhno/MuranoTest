using Autofac.Integration.WebApi;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace MuranoTest.Web.Filters
{
    public class Logger : IAutofacActionFilter, System.Web.Mvc.IActionFilter
    {
        readonly ILoggerService _logger;

        public bool AllowMultiple => throw new NotImplementedException();

        public Logger(ILoggerService logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _logger.Write(actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            _logger.Write(actionContext.ActionDescriptor.ActionName);
            return Task.FromResult(0);
        }

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            _logger.Write(actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
            return Task.FromResult(0);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _logger.Write(filterContext.ActionDescriptor.ActionName);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.Write(filterContext.ActionDescriptor.ActionName);
        }
    }
}