using System;
using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Services;
using Axon.Business.Services;
using Axon.Data.Abstractions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axon.Business.Abstractions.Extensions
{
    public static class BusinessServicesCollectionExtensions
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataLayerServices(configuration);

            services.AddSingleton<ProjectAdapter>();
            services.AddSingleton<NetworkAdapter>();
            services.AddSingleton<ServerAdapter>();
            services.AddSingleton<ProjectEnvironmentAdapter>();
            services.AddSingleton<ProjectTechnologyAdapter>();
            services.AddSingleton<TechnologyAdapter>();

            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<INetworksService, NetworksService>();
            services.AddScoped<IServersService, ServersService>();
            services.AddScoped<ITechnologiesService, TechnologiesService>();
            return services;
        }
    }
}
