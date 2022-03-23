using Medium.Users.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Medium.Users.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                try
                {
                    var context = serviceProvider.GetRequiredService<DatabaseContext>();
                    DatabaseInitializator.Initializat(context);
                }
                catch
                {
                    ILogger logger = serviceProvider.GetRequiredService<ILogger>();
                    logger.LogError("An error occurred while trying to interact with the database");
                }
            }

            host.Start();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
