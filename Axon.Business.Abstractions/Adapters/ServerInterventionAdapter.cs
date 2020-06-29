using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ServerInterventionLightAdapter : InterventionAdapter<ServerIntervention, Server, ServerInterventionDTO>
    {
        public override ServerIntervention Bind(ServerIntervention entity, ServerInterventionDTO dto)
        {
            return base.Bind(entity, dto);
        }

        public override ServerInterventionDTO Convert(ServerIntervention entity, ServerInterventionDTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Type = "server";

            return dto;
        }
    }

    public class ServerInterventionAdapter : ServerInterventionLightAdapter
    {
        public override ServerInterventionDTO Convert(ServerIntervention entity, ServerInterventionDTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Data = AdapterFactory.Get<ServerLightAdapter>().Convert(entity.Data, null);

            return dto;
        }
    }
}
