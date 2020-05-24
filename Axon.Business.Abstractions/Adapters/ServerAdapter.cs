using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ServerAdapter : IdentifiedEntityAdapter<Server, ServerDTO>
    {
        public override ServerDTO Convert(Server entity, ServerDTO dto)
        {
            dto = base.Convert(entity, dto);

            return dto;
        }

        public override Server Bind(Server entity, ServerDTO dto)
        {
            entity = base.Bind(entity, dto);
            return entity;
        }
    }
}
