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
    public class NetworkTeammatesRepository : TeammatesRepository<NetworkTeammate, Network>, INetworkTeammatesRepository
    {
        public NetworkTeammatesRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<NetworkTeammate> _loadProperties(IQueryable<NetworkTeammate> entities)
        {
            return entities.Include(t => t.User).Include(t => t.Data);
        }
    }
}
