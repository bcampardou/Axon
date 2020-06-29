using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Adapters
{
    public class ProjectInterventionLightAdapter : InterventionAdapter<ProjectIntervention, Project, ProjectInterventionDTO>
    {
        public override ProjectIntervention Bind(ProjectIntervention entity, ProjectInterventionDTO dto)
        {
            return base.Bind(entity, dto);
        }

        public override ProjectInterventionDTO Convert(ProjectIntervention entity, ProjectInterventionDTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Type = "project";

            return dto;
        }
    }

    public class ProjectInterventionAdapter : ProjectInterventionLightAdapter
    {
        public override ProjectInterventionDTO Convert(ProjectIntervention entity, ProjectInterventionDTO dto = null)
        {
            dto = base.Convert(entity, dto);
            dto.Data = AdapterFactory.Get<ProjectLightAdapter>().Convert(entity.Data, null);

            return dto;
        }
    }
}
