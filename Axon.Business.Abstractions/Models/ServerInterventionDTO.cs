using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class ServerInterventionDTO : InterventionDTO<ServerIntervention>
    {
        public ServerLightDTO Data { get; set; }
    }
}
