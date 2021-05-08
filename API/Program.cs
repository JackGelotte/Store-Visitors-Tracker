using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            DeleteCreateAndSeedDb(host).Wait();
            host.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // Deletes then/or creates database. And will seed with mock data. QoL for developing.
        public static async Task DeleteCreateAndSeedDb(IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await Seed.Seeder(services);
            }
        }
    }
}
