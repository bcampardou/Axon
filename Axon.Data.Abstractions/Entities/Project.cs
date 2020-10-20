using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Axon.Data.Abstractions.Entities
{
    public class Project : IdentifiedEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
