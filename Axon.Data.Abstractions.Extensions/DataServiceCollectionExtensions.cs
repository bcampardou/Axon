﻿using System;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using Axon.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axon.Data.Abstractions.Extensions
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, AxonDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .ConfigureWarnings(warnings =>
                          warnings.Default(WarningBehavior.Throw))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders()
                .AddSignInManager()
                .AddEntityFrameworkStores<AxonDbContext>();

            services.AddScoped<AxonDbContext>(provider => provider.GetRequiredService<DbContext>() as AxonDbContext);
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IServersRepository, ServersRepository>();
            services.AddScoped<ITechnologiesRepository, TechnologiesRepository>();
            services.AddScoped<INetworksRepository, NetworksRepository>();
            services.AddScoped<IProjectEnvironmentsRepository, ProjectEnvironmentsRepository>();
            services.AddScoped<IProjectTechnologiesRepository, ProjectTechnologiesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ITenantsRepository, TenantsRepository>();
            services.AddScoped<ILicensesRepository, LicensesRepository>();
            services.AddScoped<IProjectTeammatesRepository, ProjectTeammatesRepository>();
            services.AddScoped<INetworkTeammatesRepository, NetworkTeammatesRepository>();
            services.AddScoped<IServerTeammatesRepository, ServerTeammatesRepository>();
            services.AddScoped<INetworkInterventionsRepository, NetworkInterventionsRepository>();
            services.AddScoped<IServerInterventionsRepository, ServerInterventionsRepository>();
            services.AddScoped<IProjectInterventionsRepository, ProjectInterventionsRepository>();
            return services;
        }
    }
}
