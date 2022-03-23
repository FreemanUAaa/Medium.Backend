using Medium.Users.Application.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Medium.Users.Application
{
    public static class Middlewares
    {
        public static IApplicationBuilder AddApplicationMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}
