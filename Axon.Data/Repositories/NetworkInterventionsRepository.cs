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
    public class NetworkInterventionsRepository : InterventionsRepository<NetworkIntervention, Network>, INetworkInterventionsRepository
    {
        public NetworkInterventionsRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<NetworkIntervention> _loadProperties(IQueryable<NetworkIntervention> entities)
        {
            return entities.Include(i => i.InChargeUser)
                            .Include(i => i.Data);
        }
    }
}
