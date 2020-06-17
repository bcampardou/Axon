using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Data.Abstractions.Entities.Base;
using Axon.Data.Abstractions.Repositories;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Axon.Business.Services
{
    public class InterventionsService<T, TSUB, DTO, ADAPTER, IREPO> : Service<T, DTO, ADAPTER, IREPO>, IInterventionsService<T, TSUB, DTO>
        where T : Intervention<TSUB>, new()
        where TSUB : IdentifiedEntity
        where DTO : InterventionDTO<T>, new()
        where ADAPTER : InterventionAdapter<T, TSUB, DTO>, new()
        where IREPO : IInterventionsRepository<T, TSUB>
    {
        public InterventionsService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, Lazy<IREPO> repository) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider, repository)
        {
        }
    }
}
