using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Core.Guards;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Business.Abstractions.Adapters
{
    public abstract class EntityAdapter<T, DTO>
        where T: Entity
        where DTO: EntityDTO<T>, new()
    {
        public virtual DTO Convert(T entity, DTO dto = null)
        {
            Ensure.Arguments.ThrowIfNull(entity, nameof(entity));
            dto = dto != null ? dto : new DTO();
            dto.CreatedAt = entity.CreatedAt;
            dto.EditedAt = entity.EditedAt;
            return dto;
        }

        public virtual T Bind(T entity, DTO dto)
        {
            Ensure.Arguments.ThrowIfNull(entity, nameof(entity));
            return entity;
        }
    }

    public abstract class IdentifiedEntityAdapter<T, DTO> : EntityAdapter<T, DTO>
        where T : IdentifiedEntity
        where DTO : IdentifiedEntityDTO<T>, new()
    {
        public override DTO Convert(T entity, DTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Id = entity.Id;
            return dto;
        }

        public override T Bind(T entity, DTO dto)
        {
            entity = base.Bind(entity, dto);
            return entity;
        }
    }
}
