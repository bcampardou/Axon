using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;

namespace Axon.Data.Repositories
{
    public class LicensesRepository : RepositoryWithIdentifier<License>, ILicensesRepository
    {
        public LicensesRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<License> _loadProperties(IQueryable<License> entities)
        {
            return entities;
        }
    }
}
