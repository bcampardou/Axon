using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Entities
{
    public class Network : IdentifiedEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
