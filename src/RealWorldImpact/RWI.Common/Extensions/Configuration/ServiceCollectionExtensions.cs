using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RWI.Common.Services.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Common.Extensions.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRwiCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Singletons
            //      Singleton lifetime services are created the first time they're requested (or when ConfigureServices is run 
            //      and an instance is specified with the service registration). Every subsequent request uses the same instance. 
            //      If the app requires singleton behavior, allowing the service container to manage the service's lifetime is 
            //      recommended. Don't implement the singleton design pattern and provide user code to manage the object's 
            //      lifetime in the class.


            // Scoped
            //      Scoped lifetime services are created once per request.
            services.AddScoped<IEncryptionService, AesManagedEncryptionService>();


            // Transient
            //      Transient lifetime services are created each time they're requested. This lifetime works best for 
            //      lightweight, stateless services.
        }
    }
}
