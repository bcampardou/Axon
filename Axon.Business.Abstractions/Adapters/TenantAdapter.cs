using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class TenantAdapter : IdentifiedEntityAdapter<Tenant, TenantDTO>
    {
        public override Tenant Bind(Tenant entity, TenantDTO dto)
        {
            entity = base.Bind(entity, dto);
            entity.Name = dto.Name.Trim().ToUpper();

            return entity;
        }

        public override TenantDTO Convert(Tenant entity, TenantDTO dto = null)
        {
            dto = base.Convert(entity, dto);

            dto.Name = entity.Name;
            var licenseAdapter = AdapterFactory.Get<LicenseAdapter>();
            dto.Licenses = entity.Licenses.Select(l => licenseAdapter.Convert(l)).ToList();

            return dto;
        }
    }
}
