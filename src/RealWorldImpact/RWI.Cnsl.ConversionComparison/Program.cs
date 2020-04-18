using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RWI.Cnsl.ConversionComparison.Extensions;
using System;
using System.Threading.Tasks;

namespace RWI.Cnsl.ConversionComparison
{
    class Program
    {
        private static IConfiguration _configuration;

        static async Task Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();

            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            await serviceProvider.GetService<ConversionApplication>().Run();
        }

        static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            _configuration = new ConfigurationBuilder()
                      .Build();
            services.AddLogging();

            services.AddSingleton<IConfiguration>(factory =>
            {
                return _configuration;
            });
            services.AddSingleton<IMapper>(mapper => MapperBuilder.Build());

            services.AddTransient<ConversionApplication>();

            return services;
        }
    }
}
