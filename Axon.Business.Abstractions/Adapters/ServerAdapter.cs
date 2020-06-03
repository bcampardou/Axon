using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ServerLightAdapter : IdentifiedEntityAdapter<Server, ServerLightDTO>
    {
        public override ServerLightDTO Convert(Server entity, ServerLightDTO dto)
        {
            dto = base.Convert(entity, dto);

            dto.Name = entity.Name;
            dto.Os = entity.OS;
            dto.Version = entity.Version;
            dto.NetworkId = entity.NetworkId;

            return dto;
        }

        public override Server Bind(Server entity, ServerLightDTO dto)
        {
            entity = base.Bind(entity, dto);

            entity.Name = dto.Name.Trim();
            entity.OS = dto.Os;
            entity.Version = dto.Version;
            entity.NetworkId = dto.NetworkId;

            return entity;
        }
    }

    public class ServerAdapter : IdentifiedEntityAdapter<Server, ServerDTO>
    {
        public override ServerDTO Convert(Server entity, ServerDTO dto = null)
        {
            if (dto == null)
                dto = new ServerDTO();

            dto = new ServerLightAdapter().Convert(entity, dto) as ServerDTO;
            dto.Network = new NetworkLightAdapter().Convert(entity.Network, null);
            var projectAdapter = new ProjectLightAdapter();
            dto.Projects = entity.ProjectEnvironments?.Select(pe => projectAdapter.Convert(pe.Project, null)).ToList();

            return dto;
        }

        public override Server Bind(Server entity, ServerDTO dto)
        {
            entity = new ServerLightAdapter().Bind(entity, dto);

            return entity;
        }
    }
}
