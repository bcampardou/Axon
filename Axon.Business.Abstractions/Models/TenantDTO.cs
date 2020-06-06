using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class TenantDTO : IdentifiedEntityDTO<Tenant>
    {
        public string Name { get; set; }
        public List<LicenseDTO> Licenses { get; set; }
    }
}
