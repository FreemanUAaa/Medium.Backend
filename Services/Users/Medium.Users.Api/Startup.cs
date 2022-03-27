using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Medium.Users.Application;
using Medium.Users.Database;
using Medium.Users.Application.Common.Mapper;
using Medium.Users.Core.Interfaces.Mapper;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Medium.Users.Application.Common;
using Microsoft.IdentityModel.Tokens;

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

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(DatabaseContext).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMappingProfile).Assembly));
            });

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

            services.AddApiVersioning();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Song Service",
                    Version = "v1"
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

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors("AllowedAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
