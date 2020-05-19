using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Business.Abstractions.Models
{
    public class EntityDTO<T> where T: Entity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
    }

    public class IdentifiedEntityDTO<T> : EntityDTO<T>
        where T: IdentifiedEntity
    {
        public string Id { get; set; }
    }
}
