using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class TechnologyAdapter : IdentifiedEntityAdapter<Technology, TechnologyDTO>
    {
        public override TechnologyDTO Convert(Technology entity, TechnologyDTO dto)
        {
            dto = base.Convert(entity, dto);

            return dto;
        }

        public override Technology Bind(Technology entity, TechnologyDTO dto)
        {
            entity = base.Bind(entity, dto);
            return entity;
        }
    }
}
