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
    public class TechnologiesRepository : RepositoryWithIdentifier<Technology>, ITechnologiesRepository
    {
        public TechnologiesRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<Technology> _loadProperties(IQueryable<Technology> entities)
        {
            return entities;
        }
    }
}
