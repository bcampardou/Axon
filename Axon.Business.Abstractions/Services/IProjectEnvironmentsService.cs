using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Services
{
    public interface IProjectEnvironmentsService : IService<ProjectEnvironment, ProjectEnvironmentDTO>
    {
    }
}
