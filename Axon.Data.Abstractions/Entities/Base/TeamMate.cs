using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Data.Abstractions.Entities.Base
{
    public class TeamMate<T> : Entity
        where T: IdentifiedEntity
    {
        public Guid DataId { get; set; }
        public virtual T Data { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
