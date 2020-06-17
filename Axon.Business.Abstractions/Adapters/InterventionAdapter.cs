using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Business.Abstractions.Adapters
{
    public class InterventionAdapter<T, TSUB, DTO> : IdentifiedEntityAdapter<T, DTO>
        where T : Intervention<TSUB>
        where TSUB : IdentifiedEntity
        where DTO : InterventionDTO<T>, new()
    {
        public override T Bind(T entity, DTO dto)
        {
            entity = base.Bind(entity, dto);
            entity.Start = dto.Start;
            entity.End = dto.End;
            entity.Description = dto.Description;
            entity.DataId = dto.DataId;
            entity.InChargeUserId = dto.InChargeUserId;

            return entity;
        }

        public override DTO Convert(T entity, DTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Start = entity.Start;
            dto.End = entity.End;
            dto.Description = entity.Description;
            dto.DataId = entity.DataId;
            dto.InChargeUserId = entity.InChargeUserId;
            var userAdapter = AdapterFactory.Get<UserLightAdapter>();
            dto.InChargeUser = userAdapter.Convert(entity.InChargeUser, null);

            return dto;
        }
    }
}
