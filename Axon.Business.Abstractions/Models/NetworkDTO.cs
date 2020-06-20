using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class NetworkLightDTO : IdentifiedEntityDTO<Network>
    {
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        public string Description { get; set; }
        public string BusinessDocumentationUrl { get; set; }
        public string TechnicalDocumentationUrl { get; set; }
    }

    public class NetworkDTO : NetworkLightDTO
    {
        public List<ServerDTO> Servers { get; set; }
        public List<UserLightDTO> Team { get; set; }
    }
}
