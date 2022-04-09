using Medium.Drafts.GrpcClient.Interfaces;
using Medium.Drafts.GrpcClient.Options;
using Medium.Drafts.GrpcClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.Drafts.GrpcClient
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGrpcClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GrpcConnectionOptions>(configuration.GetSection("grpc"));

            services.AddTransient<IGrpcUserService, GrpcUserService>();

            return services;
        }
    }
}
