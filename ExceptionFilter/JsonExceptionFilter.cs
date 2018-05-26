using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventHorizon.Game.Server.Core.ExceptionFilter
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        public JsonExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(this.GetMessage(context))
            {
                StatusCode = 500,
            };
        }

        private object GetMessage(ExceptionContext context) => new
        {
            code = 500,
            message = context.Exception.Message,
            content = _env.IsProduction() ? null : context.Exception.StackTrace,
        };
    }
}