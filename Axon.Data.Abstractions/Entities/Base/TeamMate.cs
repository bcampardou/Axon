using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Data.Abstractions.Entities.Base
{
    public abstract class TeamMate<T>
        where T: IdentifiedEntity
    {
        public string DataId { get; set; }
        public virtual T Data { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
