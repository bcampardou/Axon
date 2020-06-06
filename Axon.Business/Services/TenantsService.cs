using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Core.Guards;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axon.Business.Services
{
    public class TenantsService : Service<Tenant, TenantDTO, TenantAdapter, ITenantsRepository>, ITenantsService
    {
        public TenantsService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, Lazy<ITenantsRepository> repository) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider, repository)
        {
        }

        public async Task<Tenant> FindOneByName(string name)
        {
            return await Repository.FindOneByPredicateAsync(t => t.Name == name);
        }

        protected override bool _onAfterCreateOrUpdate(Tenant entity, ActionType action)
        {
            var result = base._onAfterCreateOrUpdate(entity, action);
            var licenseRepository = _serviceProvider.GetRequiredService<ILicensesRepository>();
            var licenses = licenseRepository.FindByPredicateAsync(e => e.TenantId == entity.Id).Result;
            var envToDelete = licenses.Where(e => !entity.Licenses.Any(ne => ne.Id == e.Id)).ToList();
            if (envToDelete.Any())
            {
                licenseRepository.Delete(envToDelete);
            }
            licenseRepository.Create(entity.Licenses.Where(ne => !Ensure.Arguments.IsValidGuid(ne.Id)).ToList());
            licenseRepository.Update(entity.Licenses.Where(ne => Ensure.Arguments.IsValidGuid(ne.Id)).ToList());
            licenseRepository.SaveChanges();
            return result;
        }
    }
}
