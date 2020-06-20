using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Axon.Data.Abstractions.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public bool IsActive { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? EditedAt { get; set; }



        [InverseProperty("User")]
        public virtual Collection<ProjectTeammate> ProjectTeams { get; set; }

        [InverseProperty("User")]
        public virtual Collection<ServerTeammate> ServerTeams { get; set; }

        [InverseProperty("User")]
        public virtual Collection<NetworkTeammate> NetworkTeams { get; set; }
    }
}
