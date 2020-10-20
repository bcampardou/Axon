using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ProjectAdapter : IdentifiedEntityAdapter<Project, ProjectDTO>
    {
        public override ProjectDTO Convert(Project entity, ProjectDTO dto)
        {
            dto = base.Convert(entity, dto);

            return dto;
        }

        public override Project Bind(Project entity, ProjectDTO dto)
        {
            entity = base.Bind(entity, dto);
            return entity;
        }
    }
}
