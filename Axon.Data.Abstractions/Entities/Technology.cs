using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Entities
{
    public class Technology : IdentifiedEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string DocumentationURL { get; set; }

        [InverseProperty("Technology")]
        public virtual Collection<ProjectTechnology> ProjectTechnologies { get; set; }
    }
}
