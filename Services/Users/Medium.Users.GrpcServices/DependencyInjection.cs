using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medium.Users.GrpcServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services)
        {
            services.AddGrpc();

            return services;
        }
    }
}
