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
    public class ServerTeammatesRepository : TeammatesRepository<ServerTeammate, Server>, IServerTeammatesRepository
    {
        public ServerTeammatesRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<ServerTeammate> _loadProperties(IQueryable<ServerTeammate> entities)
        {
            return entities.Include(t => t.User).Include(t => t.Data);
        }
    }
}
