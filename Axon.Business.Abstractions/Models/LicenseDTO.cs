using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class LicenseDTO : IdentifiedEntityDTO<License>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Key { get; set; }
        public bool IsActive { get; set; }
        public string TenantId { get; set; }
    }
}
