using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Axon.Business.Services
{
    public class KnowledgeSheetsService : Service<KnowledgeSheet, KnowledgeSheetDTO, KnowledgeSheetAdapter, IKnowledgeSheetsRepository>, IKnowledgeSheetsService
    {
        public KnowledgeSheetsService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, Lazy<IKnowledgeSheetsRepository> repository) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider, repository)
        {
        }

        public async Task<List<KnowledgeSheetDTO>> FindRootAsync()
        {
            return _ToDTO(await Repository.FindByPredicateAsync(s => s.ParentId == null));
        }
    }
}
