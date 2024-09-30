using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromoCodeFactory.DataAccess;

namespace PromoCodeFactory.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var dataBase = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => 
                    { 
                        webBuilder.UseStartup<Startup>();
                        //Неясно для чего это используется в чистой архитектуре
                        webBuilder.ConfigureAppConfiguration((hostingContextn, config) =>
                        {

                        });
                    });
    }
}