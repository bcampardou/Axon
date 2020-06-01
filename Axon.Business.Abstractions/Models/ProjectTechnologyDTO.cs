using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class ProjectTechnologyDTO : EntityDTO<ProjectTechnology>
    {
        public string ProjectId { get; set; }
        public string TechnologyId { get; set; }
    }
}
