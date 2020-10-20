using System;
using Axon.Data.Abstractions.Repositories;
using Axon.Data.Repositories;
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
            services.AddScoped<AxonDbContext>(provider => provider.GetRequiredService<DbContext>() as AxonDbContext);
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            return services;
        }
    }
}
