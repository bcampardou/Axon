using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Core.Guards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Axon.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ProjectDTO>> Get([FromServices]IProjectsService service)
        {
            return await service.FindAllAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ProjectDTO> Get(Guid id, [FromServices]IProjectsService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost]
        public async Task<object> Post([FromBody]ProjectDTO project, [FromServices]IProjectsService service)
        {
            return await service.CreateOrUpdateAsync(project);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<object> Delete(Guid id, [FromServices]IProjectsService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }
    }
}