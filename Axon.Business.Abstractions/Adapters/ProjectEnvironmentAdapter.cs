using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Core.Guards;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ProjectEnvironmentAdapter : EntityAdapter<ProjectEnvironment, ProjectEnvironmentDTO>
    {
        public override ProjectEnvironmentDTO Convert(ProjectEnvironment entity, ProjectEnvironmentDTO dto)
        {
            dto = base.Convert(entity, dto);
            dto.ProjectId = entity.ProjectId;
            dto.ServerId = entity.ServerId;
            dto.Name = entity.Name;
            dto.URL = entity.URL;

            return dto;
        }

        public override ProjectEnvironment Bind(ProjectEnvironment entity, ProjectEnvironmentDTO dto)
        {
            entity = base.Bind(entity, dto);

            entity.ProjectId = Ensure.Arguments.IsValidGuid(dto.ProjectId) ? dto.ProjectId : entity.ProjectId;
            entity.ServerId = dto.ServerId;
            entity.Name = dto.Name;
            entity.URL = dto.URL;

            return entity;
        }
    }
}
