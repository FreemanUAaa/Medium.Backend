using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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


            return services;
        }
    }
}
