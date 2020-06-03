using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ProjectLightAdapter : IdentifiedEntityAdapter<Project, ProjectLightDTO>
    {
        public override ProjectLightDTO Convert(Project entity, ProjectLightDTO dto)
        {
            dto = base.Convert(entity, dto);
            dto.Name = entity.Name;
            var technologyAdapter = new TechnologyAdapter();
            dto.Technologies = entity.ProjectTechnologies?.Select(t => technologyAdapter.Convert(t.Technology, null)).ToList() ?? new List<TechnologyDTO>();

            return dto;
        }

        public override Project Bind(Project entity, ProjectLightDTO dto)
        {
            entity = base.Bind(entity, dto);
            entity.Name = dto.Name.Trim();

            return entity;
        }
    }

    public class ProjectAdapter : IdentifiedEntityAdapter<Project, ProjectDTO>
    {
        public override ProjectDTO Convert(Project entity, ProjectDTO dto)
        {
            if (dto == null)
                dto = new ProjectDTO();
            dto = new ProjectLightAdapter().Convert(entity, dto) as ProjectDTO;
            var environmentAdapter = new ProjectEnvironmentAdapter();
            dto.Environments = entity.ProjectEnvironments?.Select(pe => environmentAdapter.Convert(pe, null)).ToList() ?? new List<ProjectEnvironmentDTO>();

            return dto;
        }

        public override Project Bind(Project entity, ProjectDTO dto)
        {
            entity = new ProjectLightAdapter().Bind(entity, dto);
            var envAdapter = new ProjectEnvironmentAdapter();
            if (entity.ProjectEnvironments == null) entity.ProjectEnvironments = new Collection<ProjectEnvironment>();
            foreach (var env in dto.Environments)
            {
                var existingEnv = entity.ProjectEnvironments.FirstOrDefault(e => e.Name == e.Name);
                if (existingEnv != null)
                {
                    existingEnv = envAdapter.Bind(existingEnv, env);
                } else
                {
                    entity.ProjectEnvironments.Add(envAdapter.Bind(new ProjectEnvironment
                    {
                        ProjectId = entity.Id
                    }, env));
                }
            }

            return entity;
        }
    }
}
