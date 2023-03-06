using System.Diagnostics;

namespace Students.API.Middleware
{
    public class TimeMiddleware
    {
        private readonly RequestDelegate _next;

        public TimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch=new Stopwatch();
            watch.Start();

            await _next(context);

            watch.Stop();
            var time = watch.ElapsedMilliseconds;

            Console.WriteLine($"Request took {time} ms");
        }
    }
}
