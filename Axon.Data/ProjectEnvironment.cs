using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Entities
{
    public class ProjectEnvironment : Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public string ServerId { get; set; }
        public virtual Server Server { get; set; }
    }
}
