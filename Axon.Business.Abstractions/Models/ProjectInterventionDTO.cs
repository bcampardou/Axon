using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class ProjectInterventionDTO : InterventionDTO<ProjectIntervention>
    {
        public ProjectLightDTO Data { get; set; }
    }
}
