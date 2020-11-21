using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RedPoint.Admin.Controllers
{
    public class ErrorController : Controller
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
                default: return Problem("Unknown server error occured.", statusCode: 500);
            }
        }
    }
}