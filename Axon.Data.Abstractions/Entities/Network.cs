using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Entities
{
    public class Network : IdentifiedEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }

        [InverseProperty("Network")]
        public virtual Collection<Server> Servers { get; set; }
    }
}
