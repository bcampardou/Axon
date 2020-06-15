using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class NetworkLightAdapter : IdentifiedEntityAdapter<Network, NetworkLightDTO>
    {
        public override NetworkLightDTO Convert(Network entity, NetworkLightDTO dto)
        {
            dto = base.Convert(entity, dto);
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.BusinessDocumentationUrl = entity.BusinessDocumentationUrl;
            dto.TechnicalDocumentationUrl = entity.TechnicalDocumentationUrl;
            dto.Description = entity.Description;

            return dto;
        }

        public override Network Bind(Network entity, NetworkLightDTO dto)
        {
            entity = base.Bind(entity, dto);
            entity.Name = dto.Name.Trim();
            entity.Description = dto.Description;
            entity.BusinessDocumentationUrl = dto.BusinessDocumentationUrl;
            entity.TechnicalDocumentationUrl = dto.TechnicalDocumentationUrl;
            entity.Description = dto.Description;

            return entity;
        }
    }

    public class NetworkAdapter : IdentifiedEntityAdapter<Network, NetworkDTO>
    {
        public override NetworkDTO Convert(Network entity, NetworkDTO dto)
        {
            if (dto == null)
                dto = new NetworkDTO();
            dto = AdapterFactory.Get<NetworkLightAdapter>().Convert(entity, dto) as NetworkDTO;

            var serverAdapter = AdapterFactory.Get<ServerLightAdapter>();
            dto.Servers = entity.Servers?.Select(s => serverAdapter.Convert(s, null)).ToList();

            var userAdapter = AdapterFactory.Get<UserLightAdapter>();
            dto.Team = entity.Team?.Select(t => userAdapter.Convert(t.User)).ToList() ?? new List<UserLightDTO>();

            return dto;
        }

        public override Network Bind(Network entity, NetworkDTO dto)
        {
            entity = AdapterFactory.Get<NetworkLightAdapter>().Bind(entity, dto);

            entity.Team.Clear();

            var team = dto.Team.Select(t =>
            {
                return new NetworkTeammate
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
