using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class NetworkAdapter : IdentifiedEntityAdapter<Network, NetworkDTO>
    {
        public override NetworkDTO Convert(Network entity, NetworkDTO dto)
        {
            dto = base.Convert(entity, dto);

            return dto;
        }

        public override Network Bind(Network entity, NetworkDTO dto)
        {
            entity = base.Bind(entity, dto);
            return entity;
        }
    }
}
