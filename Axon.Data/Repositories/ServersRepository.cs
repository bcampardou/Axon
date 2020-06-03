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
    public class ServersRepository : RepositoryWithIdentifier<Server>, IServersRepository
    {
        public ServersRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<Server> _loadProperties(IQueryable<Server> entities)
        {
            return entities.Include(s => s.Network)
                            .Include(s => s.ProjectEnvironments);
        }
    }
}
