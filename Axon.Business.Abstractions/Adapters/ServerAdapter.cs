using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Business.Abstractions.Adapters.Factory;
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
            dto.Description = entity.Description;
            dto.BusinessDocumentationUrl = entity.BusinessDocumentationUrl;
            dto.TechnicalDocumentationUrl = entity.TechnicalDocumentationUrl;
            dto.Os = entity.OS;
            dto.Version = entity.Version;
            dto.NetworkId = entity.NetworkId;

            return dto;
        }

        public override Server Bind(Server entity, ServerLightDTO dto)
        {
            entity = base.Bind(entity, dto);

            entity.Name = dto.Name.Trim();
            entity.Description = dto.Description;
            entity.BusinessDocumentationUrl = dto.BusinessDocumentationUrl;
            entity.TechnicalDocumentationUrl = dto.TechnicalDocumentationUrl;
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
            dto.Network = AdapterFactory.Get<NetworkLightAdapter>().Convert(entity.Network, null);
            var projectAdapter = new ProjectLightAdapter();
            dto.Projects = entity.ProjectEnvironments?.Select(pe => projectAdapter.Convert(pe.Project, null)).ToList();

            var userAdapter = AdapterFactory.Get<UserLightAdapter>();
            dto.Team = entity.Team?.Select(t => userAdapter.Convert(t.User)).ToList() ?? new List<UserLightDTO>();

            return dto;
        }

        public override Server Bind(Server entity, ServerDTO dto)
        {
            entity = AdapterFactory.Get<ServerLightAdapter>().Bind(entity, dto);

            entity.Team.Clear();
            var team = dto.Team.Select(t =>
            {
                return new ServerTeammate
                {
                    DataId = entity.Id,
                    UserId = t.Id
                };
            });
            foreach (var member in team)
            {
                entity.Team.Add(member);
            }

            return entity;
        }
    }
}
