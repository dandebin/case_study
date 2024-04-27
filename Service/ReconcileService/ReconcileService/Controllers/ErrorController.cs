using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ReconcileService.Controllers
{
    /// <summary>
    /// Controller class for handling error responses.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
		private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
		{
			_logger = logger;
		}

        /// <summary>
        /// Handles error responses in a development environment by providing detailed error information.
        /// This method is only accessible in development environments.
        /// </summary>
        /// <param name="hostEnvironment">The IHostEnvironment instance to check the current environment.</param>
        /// <returns>An IActionResult representing the error response with detailed information (in development environment) or NotFound (in production).</returns>
        [HttpGet]
        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature?.Error?.StackTrace,
                title: exceptionHandlerFeature?.Error?.Message);
        }

        /// <summary>
        /// Handles error responses in a production environment by providing a generic error message.
        /// </summary>
        /// <returns>An IActionResult representing the error response with a generic error message.</returns>
        [HttpGet]
        [Route("/error")]
        public IActionResult HandleError() =>
            Problem();
    }
}

