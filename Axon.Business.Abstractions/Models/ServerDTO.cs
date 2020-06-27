using System;
using System.Collections.Generic;
using System.Text;
using Axon.Core.Enums;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class ServerLightDTO : IdentifiedEntityDTO<Server>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BusinessDocumentationUrl { get; set; }
        public string TechnicalDocumentationUrl { get; set; }

        public OperatingSystems Os { get; set; }
        public string Version { get; set; }
        public Guid NetworkId { get; set; }
    }

    public class ServerDTO : ServerLightDTO
    {
        public List<ProjectLightDTO> Projects { get; set; }
        public NetworkLightDTO Network { get; set; }
        public List<UserLightDTO> Team { get; set; }
        public List<ServerInterventionDTO> Interventions { get; set; }
    }
}
