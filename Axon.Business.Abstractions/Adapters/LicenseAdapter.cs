using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class LicenseAdapter : IdentifiedEntityAdapter<License, LicenseDTO>
    {
        public override License Bind(License entity, LicenseDTO dto)
        {
            entity = base.Bind(entity, dto);
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.IsActive = dto.IsActive;
            entity.Key = dto.Key;
            entity.TenantId = dto.TenantId;
            return entity;
        }

        public override LicenseDTO Convert(License entity, LicenseDTO dto = null)
        {
            dto = base.Convert(entity, dto);

            dto.StartDate = entity.StartDate;
            dto.EndDate = entity.EndDate;
            dto.IsActive = entity.IsActive;
            dto.Key = entity.Key;
            dto.TenantId = entity.TenantId;

            return dto;
        }
    }
}
