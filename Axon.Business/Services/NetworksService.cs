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
    public class NetworksService : Service<Network, NetworkDTO, NetworkAdapter, INetworksRepository>, INetworksService
    {
        public NetworksService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, Lazy<INetworksRepository> repository) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider, repository)
        {
        }

        protected override bool _onAfterCreateOrUpdate(Network entity, ActionType action)
        {
            var result = base._onAfterCreateOrUpdate(entity, action);

            var teamRepository = _serviceProvider.GetRequiredService<INetworkTeammatesRepository>();
            var team = teamRepository.FindByPredicateAsync(e => e.DataId == entity.Id).Result;
            var teamToDelete = team.Where(e => !entity.Team.Any(ne => ne.UserId == e.UserId)).ToList();
            if (teamToDelete.Any())
            {
                teamRepository.Delete(teamToDelete);
            }
            teamRepository.Create(entity.Team.Where(ne => !team.Any(e => e.UserId == ne.UserId)).ToList());
            teamRepository.Update(entity.Team.Where(ne => team.Any(e => e.UserId == ne.UserId)).ToList());
            teamRepository.SaveChanges();

            return result;
        }
    }
}
