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
    public class NetworksRepository : RepositoryWithIdentifier<Network>, INetworksRepository
    {
        public NetworksRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<Network> _loadProperties(IQueryable<Network> entities)
        {
            var now = DateTime.Today;
            return entities.Include(n => n.Servers).ThenInclude(s => s.ProjectEnvironments).ThenInclude(e => e.Project).ThenInclude(p => p.ProjectTechnologies).ThenInclude(pt => pt.Technology)
                            .Include(n => n.Servers).ThenInclude(s => s.ProjectEnvironments).ThenInclude(e => e.Project).ThenInclude(p => p.Interventions).ThenInclude(i => i.InChargeUser)
                            .Include(n => n.Servers).ThenInclude(s => s.Team).ThenInclude(t => t.User)
                            .Include(n => n.Servers).ThenInclude(s => s.Interventions).ThenInclude(i => i.InChargeUser)
                            .Include(n => n.Interventions).ThenInclude(i => i.InChargeUser)
                            .Include(p => p.Team).ThenInclude(t => t.User);
        }
    }
}
