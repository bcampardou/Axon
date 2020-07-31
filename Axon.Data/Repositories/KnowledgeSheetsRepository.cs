using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axon.Data.Repositories
{
    public class KnowledgeSheetsRepository : RepositoryWithIdentifier<KnowledgeSheet>, IKnowledgeSheetsRepository
    {
        public KnowledgeSheetsRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        protected override IQueryable<KnowledgeSheet> _loadProperties(IQueryable<KnowledgeSheet> entities)
        {
            return entities.Include(s => s.Children).ThenInclude(s => s.Children).ThenInclude(s => s.Children).ThenInclude(s => s.Children).ThenInclude(s => s.Children)
                .Include(s => s.Parent);
        }
    }
}
