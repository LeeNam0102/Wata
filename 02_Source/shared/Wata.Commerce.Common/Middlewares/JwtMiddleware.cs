using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Wata.Commerce.Common.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<JwtMiddleware> logger)
        {
            //logic here

            await _next(context);
        }
    }
}