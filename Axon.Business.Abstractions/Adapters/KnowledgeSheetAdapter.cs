using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axon.Business.Abstractions.Adapters
{
    public class KnowledgeSheetAdapter : IdentifiedEntityAdapter<KnowledgeSheet, KnowledgeSheetDTO>
    {
        public override KnowledgeSheet Bind(KnowledgeSheet entity, KnowledgeSheetDTO dto)
        {
            entity = base.Bind(entity, dto);
            entity.Title = dto.Title;
            entity.Document = dto.Document;
            entity.ParentId = dto.ParentId;

            return entity;
        }

        public override KnowledgeSheetDTO Convert(KnowledgeSheet entity, KnowledgeSheetDTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Title = entity.Title;
            dto.Document = entity.Document;
            dto.ParentId = entity.ParentId;
            dto.Children = entity.Children != null ? entity.Children.Select(c => Convert(c)).ToList() : new List<KnowledgeSheetDTO>();

            return dto;
        }
    }
}
