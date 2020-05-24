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
    public class ProjectEnvironmentsRepository : Repository<ProjectEnvironment>, IProjectEnvironmentsRepository
    {
        public ProjectEnvironmentsRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<ProjectEnvironment> _loadProperties(IQueryable<ProjectEnvironment> entities)
        {
            return entities;
        }
    }
}
