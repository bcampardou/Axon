using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Entities
{
    public class License : IdentifiedEntity
    {
        public string Key { get; set; }
        public int NumberOfAllowedUsers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
