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

            dto.TechnologyId = entity.TechnologyId;
            dto.ProjectId = entity.ProjectId;

            return dto;
        }

        public override ProjectTechnology Bind(ProjectTechnology entity, ProjectTechnologyDTO dto)
        {
            entity = base.Bind(entity, dto);

            entity.TechnologyId = dto.TechnologyId;
            entity.ProjectId = dto.ProjectId;

            return entity;
        }
    }
}
