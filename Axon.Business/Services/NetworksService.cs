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
    public class NetworksService : Service<Network, NetworkDTO, NetworkAdapter, INetworksRepository>, INetworksService
    {
        public NetworksService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, NetworkAdapter adapter, Lazy<INetworksRepository> repository) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider, adapter, repository)
        {
        }
    }
}
