using Axon.Data.Abstractions.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Axon.Data.Abstractions.Entities
{
    public class KnowledgeSheet : IdentifiedEntity
    {
        public string Title { get; set; }
        public string Document { get; set; }

        public Guid? ParentId { get; set; }
        public virtual KnowledgeSheet Parent { get; set; }

        [InverseProperty("Parent")]
        public virtual ICollection<KnowledgeSheet> Children { get; set; }
    }
}
