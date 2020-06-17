using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Entities.Base;
using Axon.Data.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Axon.Data.Repositories
{
    public class ServerInterventionsRepository : InterventionsRepository<ServerIntervention, Server>, IServerInterventionsRepository
    {
        public ServerInterventionsRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<ServerIntervention> _loadProperties(IQueryable<ServerIntervention> entities)
        {
            return entities.Include(i => i.InChargeUser)
                            .Include(i => i.Data);
        }
    }
}
