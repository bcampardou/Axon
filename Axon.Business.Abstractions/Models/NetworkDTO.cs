using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class NetworkLightDTO : IdentifiedEntityDTO<Network>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class NetworkDTO : NetworkLightDTO
    {
        public List<ServerLightDTO> Servers { get; set; }
    }
}
