using Medium.Desings.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.Desings.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connection)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connection));
            services.AddTransient<IDatabaseContext, DatabaseContext>();

            return services;
        }
    }
}
