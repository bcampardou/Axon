using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Entities
{
    public class Tenant : IdentifiedEntity
    {
        public string Name { get; set; }
        public string SecretKey { get; set; }
        
        [InverseProperty("Tenant")]
        public virtual Collection<License> Licenses { get; set; }

        [InverseProperty("Tenant")]
        public virtual Collection<Network> Networks { get; set; }

        [InverseProperty("Tenant")]
        public virtual Collection<User> Users { get; set; }
    }
}
