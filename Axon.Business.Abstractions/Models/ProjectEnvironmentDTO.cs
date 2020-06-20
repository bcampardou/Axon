using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class ProjectEnvironmentDTO : EntityDTO<ProjectEnvironment>
    {

        public string Name { get; set; }

        public string URL { get; set; }
        public Guid ServerId { get; set; }

        public ServerLightDTO Server {get;set;}
        public Guid ProjectId { get; set; }
    }
}
