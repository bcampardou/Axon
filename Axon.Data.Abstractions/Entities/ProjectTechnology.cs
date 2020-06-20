using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Entities
{
    public class ProjectTechnology : Entity
    {
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public Guid TechnologyId { get; set; }
        public virtual Technology Technology { get; set; }
    }
}
