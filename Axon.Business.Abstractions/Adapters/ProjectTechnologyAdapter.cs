using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ProjectTechnologyAdapter : EntityAdapter<ProjectTechnology, ProjectTechnologyDTO>
    {
        public override ProjectTechnologyDTO Convert(ProjectTechnology entity, ProjectTechnologyDTO dto)
        {
            dto = base.Convert(entity, dto);

            return dto;
        }

        public override ProjectTechnology Bind(ProjectTechnology entity, ProjectTechnologyDTO dto)
        {
            entity = base.Bind(entity, dto);
            return entity;
        }
    }
}
