using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Azure.Identity;

namespace FlightsToCharts.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // In case of migration, this needs to be commented until next comment
                //.ConfigureAppConfiguration((context, config) =>
                //{
                //   var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("SepAirlineKeyVaultURI"));
                //   config.AddAzureKeyVault(
                //   keyVaultEndpoint,
                //   new DefaultAzureCredential());
                //})
                // Until here !!
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
