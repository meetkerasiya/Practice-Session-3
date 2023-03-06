using System.Text;
using Serilog;

namespace Students.API.Middleware
{
    public class RequestResponseLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            // Log the request
            Log.Information("Request {RequestMethod} {RequestPath} received", request.Method, request.Path);

            // Copy the response body to a memory stream for logging
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Call the next middleware in the pipeline
            await _next(context);

            // Log the response
            Log.Information("Response {RequestMethod} {RequestPath} returned {StatusCode}", request.Method, request.Path, context.Response.StatusCode);

            // Write the response body back to the original response stream
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;


        }

       
    }
}
