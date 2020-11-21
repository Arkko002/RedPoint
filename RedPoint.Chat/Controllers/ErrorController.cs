using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Chat.Exceptions.Security;

namespace RedPoint.Chat.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/error-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException("Tried invoking development error method in non-development environment.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(context.Error.StackTrace, title: context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            switch (exception)
            {
                case LackOfPermissionException p: return Problem(p.Message, statusCode: 403);
                case InvalidChannelRequestException c: return Problem(c.Message, statusCode: 403);
                case InvalidServerRequestException s: return Problem(s.Message, statusCode: 403);
                default: return Problem("Unknown server error occured.", statusCode: 500);
            }
        }
    }
}