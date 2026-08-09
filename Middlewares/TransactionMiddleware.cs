using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate next;

        public TransactionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var apikey = context.Request.Query["api_key"];
            if (string.IsNullOrWhiteSpace(apikey))
            {
                await context.Response.WriteAsync("API Key Is Required");
                return;
            }
            context.Response.Headers.Append("X-Transactions-Id",Guid.NewGuid().ToString());
            await next(context);
        }

    }
    public static class TransactionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TransactionMiddleware>();
        }
    }

}