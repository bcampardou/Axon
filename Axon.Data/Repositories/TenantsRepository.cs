using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Axon.Data.Repositories
{
    public class TenantsRepository : RepositoryWithIdentifier<Tenant>, ITenantsRepository
    {
        public TenantsRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<Tenant> _loadProperties(IQueryable<Tenant> entities)
        {
            return entities.Include(t => t.Licenses)
                .Include(t => t.Networks).ThenInclude(n => n.Servers)
                .Include(t => t.Users);
        }
    }
}
