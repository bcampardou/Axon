using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Business.Abstractions.Models
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
