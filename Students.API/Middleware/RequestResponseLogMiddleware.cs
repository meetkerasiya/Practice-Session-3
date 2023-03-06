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
            //for request logs
            var request = await RequestFormatter(context.Request);
            Log.Information($"Request {request}");
            Console.WriteLine("req res middleware");
            Console.WriteLine(context.Request.Body.ToString());


            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await _next(context);

            //for response log
           
            
            var originalBodyStream = context.Response.Body;
            
            var response=await ResponseFormatter(context.Response);
            Log.Information($"Response: {response}");

            await responseBody.CopyToAsync(originalBodyStream);
            
        }

        //private async Task<string> RequestFormatter(HttpRequest request)
        //{
        //    var body = request.Body;
        //    request.EnableBuffering();
        //    var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        //    await request.Body.ReadAsync(buffer, 0, buffer.Length);
        //    var bodyAsText = Encoding.UTF8.GetString(buffer);
        //    request.Body = body;

        //    return $"{request.Method} {request.Host} {request.Path} {request.QueryString} {bodyAsText}";
        //}

        //private async Task<string> ResponseFormatter(HttpResponse response)
        //{
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    var bodyStream=await new StreamReader(response.Body).ReadToEndAsync();
        //    response.Body.Seek(0,SeekOrigin.Begin);
        //    return $"{response.StatusCode}: {bodyStream}";
        //}
    }
}
