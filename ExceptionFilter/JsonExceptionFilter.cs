namespace EventHorizon.Game.Server.Core.ExceptionFilter
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        public JsonExceptionFilter(IWebHostEnvironment env)
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