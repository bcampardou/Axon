﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string NetworkId { get; set; }
        public virtual Network Network { get; set; }

        [InverseProperty("Server")]
        public virtual Collection<ProjectEnvironment> ProjectEnvironments { get; set; }
    }
}
