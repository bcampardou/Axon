using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Axon.Core.Enums;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Entities
{
    public class Server : IdentifiedEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }

        public OperatingSystems OS { get; set; }
        public string Version { get; set; }
    }
}
