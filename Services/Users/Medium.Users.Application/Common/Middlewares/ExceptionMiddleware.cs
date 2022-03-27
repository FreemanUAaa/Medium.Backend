using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Medium.Users.Application.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext http)
        {
            try
            {
                await next.Invoke(http);
            }
            catch (Exception error)
            {
                http.Response.StatusCode = StatusCodes.Status400BadRequest;
                string errorMessage = $"{{\"error\": \"{error.Message}\" }}";
                await http.Response.WriteAsync(errorMessage);
            }
        }
    }
}
