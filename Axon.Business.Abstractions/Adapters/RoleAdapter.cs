using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Core.Guards;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class RoleAdapter
    {
        public RoleDTO Convert(Role entity, RoleDTO dto = null)
        {
            Ensure.Arguments.ThrowIfNull(entity, nameof(entity));
            dto = dto != null ? dto : new RoleDTO();
            dto.Id = entity.Id;
            dto.CreatedAt = entity.CreatedAt;
            dto.EditedAt = entity.EditedAt;
            dto.Name = entity.Name;
            dto.NormalizedName = entity.NormalizedName;
            return dto;
        }

        public Role Bind(Role entity, RoleDTO dto)
        {
            Ensure.Arguments.ThrowIfNull(entity, nameof(entity));
            entity.Name = dto.Name;
            entity.NormalizedName = dto.NormalizedName;
            return entity;
        }
    }
}
