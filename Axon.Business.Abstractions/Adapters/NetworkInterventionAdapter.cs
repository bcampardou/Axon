using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class NetworkInterventionLightAdapter : InterventionAdapter<NetworkIntervention, Network, NetworkInterventionDTO>
    {
        public override NetworkIntervention Bind(NetworkIntervention entity, NetworkInterventionDTO dto)
        {
            return base.Bind(entity, dto);
        }

        public override NetworkInterventionDTO Convert(NetworkIntervention entity, NetworkInterventionDTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Type = "network";

            return dto;
        }
    }

    public class NetworkInterventionAdapter : NetworkInterventionLightAdapter
    {
        public override NetworkInterventionDTO Convert(NetworkIntervention entity, NetworkInterventionDTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Data = AdapterFactory.Get<NetworkLightAdapter>().Convert(entity.Data, null);

            return dto;
        }
    }
}
