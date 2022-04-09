using Medium.Drafts.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Medium.Drafts.Database
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
