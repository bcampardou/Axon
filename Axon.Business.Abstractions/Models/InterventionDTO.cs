using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Business.Abstractions.Models
{
    public class InterventionDTO<T> : IdentifiedEntityDTO<T>
        where T: IdentifiedEntity
    {
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string InChargeUserId { get; set; }
        public UserLightDTO InChargeUser { get; set; }
        public string DataId { get; set; }
    }
}
