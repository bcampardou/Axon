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
    public class TechnologiesService : Service<Technology, TechnologyDTO, TechnologyAdapter, ITechnologiesRepository>, ITechnologiesService
    {
        public TechnologiesService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, TechnologyAdapter adapter, Lazy<ITechnologiesRepository> repository) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider, adapter, repository)
        {
        }
    }
}
