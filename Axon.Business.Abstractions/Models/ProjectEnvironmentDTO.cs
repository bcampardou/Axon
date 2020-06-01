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
        public string ServerId { get; set; }

        public ServerLightDTO Server {get;set;}
        public string ProjectId { get; set; }
    }
}
