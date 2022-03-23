using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Medium.Users.Application;
using Medium.Users.Database;

namespace Medium.Users.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("SqlServer");

            services.AddApplication(Configuration);
            services.AddDatabase(connection);

            services.AddControllers();

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowedAll", opt =>
                {
                    opt.AllowAnyMethod();
                    opt.AllowAnyHeader();
                    opt.AllowAnyOrigin();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.AddApplicationMiddlewares();

            app.UseHttpsRedirection();

            app.UseCors("AllowedAll");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
