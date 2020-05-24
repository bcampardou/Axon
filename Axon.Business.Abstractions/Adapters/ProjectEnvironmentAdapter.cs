using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ProjectEnvironmentAdapter : EntityAdapter<ProjectEnvironment, ProjectEnvironmentDTO>
    {
        public override ProjectEnvironmentDTO Convert(ProjectEnvironment entity, ProjectEnvironmentDTO dto)
        {
            dto = base.Convert(entity, dto);

            return dto;
        }

        public override ProjectEnvironment Bind(ProjectEnvironment entity, ProjectEnvironmentDTO dto)
        {
            entity = base.Bind(entity, dto);
            return entity;
        }
    }
}
