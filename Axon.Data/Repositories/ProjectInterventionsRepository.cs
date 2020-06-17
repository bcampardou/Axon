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
    public class ProjectInterventionsRepository : InterventionsRepository<ProjectIntervention, Project>, IProjectInterventionsRepository
    {
        public ProjectInterventionsRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<ProjectIntervention> _loadProperties(IQueryable<ProjectIntervention> entities)
        {
            return entities.Include(i => i.InChargeUser)
                            .Include(i => i.Data);
        }
    }
}
