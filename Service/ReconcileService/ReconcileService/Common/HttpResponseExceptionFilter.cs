using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReconcileService
{
    /// <summary>
    /// Action filter for handling HttpResponseException exceptions thrown during controller action execution.
    /// </summary>
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        /// <summary>
        /// Gets the order of execution for this filter. Higher values execute later in the pipeline.
        /// </summary>
        public int Order => int.MaxValue - 10;

        /// <summary>
        /// Invoked before the controller action is executed. (Empty implementation in this case)
        /// </summary>
        /// <param name="context">The ActionExecutingContext containing information about the action execution.</param>
        public void OnActionExecuting(ActionExecutingContext context) { }

        /// <summary>
        /// Invoked after the controller action is executed.
        /// This method checks for HttpResponseException and converts it to an ObjectResult with appropriate status code and optional error details.
        /// </summary>
        /// <param name="context">The ActionExecutedContext containing information about the action execution and any exceptions.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Value)
                {
                    StatusCode = httpResponseException.StatusCode
                };

                context.ExceptionHandled = true;
            }
        }
    }
}

