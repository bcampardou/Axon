using System;
using System.IO;
using Axon.Business.Abstractions.Services;
using Axon.Business.Services;
using Axon.Data.Abstractions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;

namespace Axon.Business.Abstractions.Extensions
{
    public static class BusinessServicesCollectionExtensions
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataLayerServices(configuration);
            
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<INetworksService, NetworksService>();
            services.AddScoped<IServersService, ServersService>();
            services.AddScoped<ITechnologiesService, TechnologiesService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ITenantsService, TenantsService>();
            services.AddScoped<ILicensesService, LicensesService>();
            services.AddSingleton<RazorLightEngine>((opts) =>
            {
                return new RazorLightEngineBuilder()
              .UseFileSystemProject($"{Directory.GetCurrentDirectory()}/Views/EmailsTemplates")
              .UseMemoryCachingProvider()
              .Build();
            });
            return services;
        }
    }
}
