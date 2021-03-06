﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Axon.Data.Repositories
{
    public class ProjectsRepository : Repository<Project>, IProjectsRepository
    {
        public ProjectsRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<Project> _loadProperties(IQueryable<Project> entities)
        {
            return entities;
        }
    }
}
