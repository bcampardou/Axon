using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Axon.Business.Services
{
    public class ProjectsService : Service<Project, ProjectDTO, ProjectAdapter, IProjectsRepository>, IProjectsService
    {
        public ProjectsService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, ProjectAdapter adapter, Lazy<IProjectsRepository> repository) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider, adapter, repository)
        {
        }
    }
}
