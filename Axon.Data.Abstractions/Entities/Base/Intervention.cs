using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Data.Abstractions.Entities.Base
{
    public class Intervention<T> : IdentifiedEntity
        where T : IdentifiedEntity
    {
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string InChargeUserId { get; set; }
        public virtual User InChargeUser { get; set; }
        public string DataId { get; set; }
        public virtual T Data { get; set; }
    }
}
