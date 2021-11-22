using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

namespace AzureAppConfigurationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration(AddAppConfigFromAzure).UseStartup<Startup>());

        private static void AddAppConfigFromAzure(IConfigurationBuilder config)
        {
            var appSettings = config.Build();
            var azureConfigStoreConnection = appSettings.GetConnectionString("AzureAppConfigurationEndPoint");

            config.AddAzureAppConfiguration(_=>
            {
                _.Connect(azureConfigStoreConnection)
                    .Select(KeyFilter.Any,LabelFilter.Null)
                    .ConfigureRefresh(r =>r.Register("AzureAppDemo:Sentinel", refreshAll: true).SetCacheExpiration(TimeSpan.FromSeconds(30)));
            });
        }
    }
}
