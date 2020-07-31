using Axon.Data.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Business.Abstractions.Models
{
    public class KnowledgeSheetDTO : IdentifiedEntityDTO<KnowledgeSheet>
    {
        public string Title { get; set; }
        public string Document { get; set; }

        public Guid? ParentId { get; set; }
        public List<KnowledgeSheetDTO> Children { get; set; }
    }
}
