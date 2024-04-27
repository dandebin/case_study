using System;
namespace ReconcileService
{
    /// <summary>
    /// Exception class specifically designed for throwing HTTP response related errors.
    /// </summary>
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpResponseException class with the specified status code.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to be included in the exception.</param>
        /// <param name="value">Optional object to be included in the exception (e.g., error details).</param>
        public HttpResponseException(int statusCode, object? value = null) =>
            (StatusCode, Value) = (statusCode, value);

        /// <summary>
        /// Gets the HTTP status code associated with the exception.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Gets an optional object that may contain additional error details.
        /// </summary>
        public object? Value { get; }
    }
}

