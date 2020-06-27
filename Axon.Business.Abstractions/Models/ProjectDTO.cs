using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class ProjectLightDTO : IdentifiedEntityDTO<Project>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BusinessDocumentationUrl { get; set; }
        public string TechnicalDocumentationUrl { get; set; }
        public List<TechnologyDTO> Technologies { get; set; }
        public List<ProjectInterventionDTO> Interventions { get; set; }
    }

    public class ProjectDTO : ProjectLightDTO
    {
        public List<ProjectEnvironmentDTO> Environments { get; set; }
        public List<UserLightDTO> Team { get; set; }
    }
}
