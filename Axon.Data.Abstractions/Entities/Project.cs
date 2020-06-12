using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Axon.Data.Abstractions.Entities
{
    public class Project : IdentifiedEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string BusinessDocumentationUrl { get; set; }
        public string TechnicalDocumentationUrl { get; set; }

        [InverseProperty("Project")]
        public virtual Collection<ProjectTechnology> ProjectTechnologies { get; set; }

        [InverseProperty("Project")]
        public virtual Collection<ProjectEnvironment> ProjectEnvironments { get; set; }

        [InverseProperty("Data")]
        public virtual Collection<ProjectTeammate> Team { get; set; }
    }
}
