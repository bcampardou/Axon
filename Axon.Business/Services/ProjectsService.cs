using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axon.Business.Services
{
    public class ProjectsService : Service<Project, ProjectDTO, ProjectAdapter, IProjectsRepository>, IProjectsService
    {
        public ProjectsService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, Lazy<IProjectsRepository> repository) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider, repository)
        {
        }

        protected override bool _onAfterCreateOrUpdate(Project entity, ActionType action)
        {
            var result = base._onAfterCreateOrUpdate(entity, action);

            var envRepository = _serviceProvider.GetRequiredService<IProjectEnvironmentsRepository>();
            var environments = envRepository.FindByPredicateAsync(e => e.ProjectId == entity.Id).Result;
            var envToDelete = environments.Where(e => !entity.ProjectEnvironments.Any(ne => ne.Name == e.Name)).ToList();
            if(envToDelete.Any())
            {
                envRepository.Delete(envToDelete);
            }
            envRepository.Create(entity.ProjectEnvironments.Where(ne => !environments.Any(e => e.Name == ne.Name)).ToList());
            envRepository.Update(entity.ProjectEnvironments.Where(ne => environments.Any(e => e.Name == ne.Name)).ToList());
            envRepository.SaveChanges();

            var teamRepository = _serviceProvider.GetRequiredService<IProjectTeammatesRepository>();
            var team = teamRepository.FindByPredicateAsync(e => e.DataId == entity.Id).Result;
            var teamToDelete = team.Where(e => !entity.Team.Any(ne => ne.UserId == e.UserId)).ToList();
            if (teamToDelete.Any())
            {
                teamRepository.Delete(teamToDelete);
            }
            teamRepository.Create(entity.Team.Where(ne => !team.Any(e => e.UserId == ne.UserId)));
            teamRepository.Update(entity.Team.Where(ne => team.Any(e => e.UserId == ne.UserId)));
            teamRepository.SaveChanges();

            return result;
        }
    }
}
