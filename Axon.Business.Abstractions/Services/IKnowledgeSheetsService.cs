using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Axon.Business.Abstractions.Services
{
    public interface IKnowledgeSheetsService : IService<KnowledgeSheet, KnowledgeSheetDTO>
    {
        Task<List<KnowledgeSheetDTO>> FindRootAsync();
    }
}
