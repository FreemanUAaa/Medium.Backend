using FluentValidation;
using MediatR;
using Medium.Users.Application.Common;
using Medium.Users.Application.Common.Behaviors;
using Medium.Users.Application.Services;
using Medium.Users.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Medium.Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            string redisConnection = configuration.GetConnectionString("Redis");

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = redisConnection;
                opt.InstanceName = "UsersCache";
            });

            services.AddTransient<IFileManager>((services) => new FileManager(
                @"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Api\wwwroot\BioPhotos\",
                @"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Api\wwwroot\UserPhotos\"
            ));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            AuthOptions authOptions = configuration.GetSection("Auth").Get<AuthOptions>();

            services.Configure<AuthOptions>(configuration.GetSection("Auth"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                       .AddJwtBearer(options =>
                       {
                           options.RequireHttpsMetadata = false;
                           options.TokenValidationParameters = new TokenValidationParameters
                           {
                               ValidateIssuer = true,
                               ValidateAudience = true,
                               ValidateLifetime = true,
                               ValidateIssuerSigningKey = true,
                               ValidIssuer = authOptions.Issuer,
                               ValidAudience = authOptions.Audience,
                               IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                           };
                       });

            return services;
        }
    }
}
