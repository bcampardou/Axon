using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Axon.Data.Abstractions.Entities.Base
{
    public class IdentifiedEntity : Entity
    {
        [Key]
        [MaxLength(36)]
        public Guid Id { get; set; }
    }
}
