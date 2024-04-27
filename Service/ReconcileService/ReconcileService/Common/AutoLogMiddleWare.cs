using System;
using System.Text;

namespace ReconcileService
{
    /// <summary>
    /// Middleware class for automatically logging HTTP request and response information.
    /// </summary>
    public class AutoLogMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AutoLogMiddleWare> _logger;

        /// <summary>
        /// Constructor for AutoLogMiddleWare.
        /// </summary>
        /// <param name="next">The next delegate in the middleware pipeline.</param>
        /// <param name="logger">The injected ILogger instance for logging.</param>
        public AutoLogMiddleWare(RequestDelegate next, ILogger<AutoLogMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware and processes the HTTP request asynchronously.
        /// </summary>
        /// <param name="context">The HttpContext instance containing request and response information.</param>
        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);

            var originalResponseBody = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next.Invoke(context);

                await LogResponse(context, responseBody, originalResponseBody);
            }
        }

        /// <summary>
        /// Logs details about the outgoing HTTP response (to be implemented).
        /// </summary>
        /// <param name="context">The HttpContext instance containing response information.</param>
        /// <param name="responseBody">The MemoryStream that captured the response body.</param>
        /// <param name="originalResponseBody">The original response body stream (to be restored).</param>
        private async Task LogResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody)
        {
            var responseContent = new StringBuilder();
            responseContent.AppendLine("=== Response Info ===");

            responseContent.AppendLine("-- headers");
            foreach (var (headerKey, headerValue) in context.Response.Headers)
            {
                responseContent.AppendLine($"header = {headerKey}    value = {headerValue}");
            }

            responseContent.AppendLine("-- body");
            responseBody.Position = 0;
            var content = await new StreamReader(responseBody).ReadToEndAsync();
            responseContent.AppendLine($"body = {content}");
            responseBody.Position = 0;
            await responseBody.CopyToAsync(originalResponseBody);
            context.Response.Body = originalResponseBody;

            _logger.LogInformation(responseContent.ToString());
        }

        /// <summary>
        /// Logs details about the outgoing HTTP request (to be implemented).
        /// </summary>
        /// <param name="context">The HttpContext instance containing response information.</param>
        private async Task LogRequest(HttpContext context)
        {
            var requestContent = new StringBuilder();

            requestContent.AppendLine("=== Request Info ===");
            requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
            requestContent.AppendLine($"path = {context.Request.Path}");

            requestContent.AppendLine("-- headers");
            foreach (var (headerKey, headerValue) in context.Request.Headers)
            {
                requestContent.AppendLine($"header = {headerKey}    value = {headerValue}");
            }

            requestContent.AppendLine("-- body");
            context.Request.EnableBuffering();
            var requestReader = new StreamReader(context.Request.Body);
            var content = await requestReader.ReadToEndAsync();
            requestContent.AppendLine($"body = {content}");

            _logger.LogInformation(requestContent.ToString());
            context.Request.Body.Position = 0;
        }
    }
}

