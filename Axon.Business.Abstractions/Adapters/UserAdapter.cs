using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Core.Guards;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class UserLightAdapter
    {
        public virtual UserLightDTO Convert(User entity, UserLightDTO dto = null)
        {
            Ensure.Arguments.ThrowIfNull(entity, nameof(entity));
            dto = dto != null ? dto : new UserLightDTO();
            dto.CreatedAt = entity.CreatedAt;
            dto.EditedAt = entity.EditedAt;
            dto.Id = entity.Id;
            dto.UserName = entity.UserName;
            dto.Email = entity.Email;
            dto.IsActive = entity.IsActive;
            dto.PhoneNumber = entity.PhoneNumber;
            dto.SecurityStamp = entity.SecurityStamp ?? "";
            dto.ConcurrencyStamp = entity.ConcurrencyStamp;
            dto.TenantId = entity.TenantId;

            return dto;
        }

        public User Bind(User entity, UserLightDTO dto)
        {
            Ensure.Arguments.ThrowIfNull(entity, nameof(entity));
            entity.UserName = dto.UserName;
            entity.Email = dto.Email;
            entity.IsActive = dto.IsActive;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.SecurityStamp = dto.SecurityStamp ?? "";
            entity.ConcurrencyStamp = dto.ConcurrencyStamp;
            entity.TenantId = Ensure.Arguments.IsValidGuid(entity.TenantId) ? entity.TenantId : dto.TenantId;

            return entity;
        }
    }

    public class UserAdapter : UserLightAdapter
    {
        public User Bind(User entity, UserDTO dto)
        {
            entity = base.Bind(entity, dto);

            return entity;
        }

        public UserDTO Convert(User entity, UserDTO dto = null)
        {
            Ensure.Arguments.ThrowIfNull(entity, nameof(entity));

            dto = dto != null ? dto : new UserDTO();
            dto = base.Convert(entity, dto) as UserDTO;
            dto.Tenant = entity.Tenant != null ? AdapterFactory.Get<TenantAdapter>().Convert(entity.Tenant) : null; 

            return dto;
        }
    }
}
