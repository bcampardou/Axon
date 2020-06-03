using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Core.Guards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Axon.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ProjectEnvironmentDTO>> Get([FromServices]IProjectEnvironmentsService service)
        {
            return await service.FindAllAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ProjectEnvironmentDTO> Get(string id, [FromServices]IProjectEnvironmentsService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost]
        public async Task<object> Post([FromBody]ProjectEnvironmentDTO Environment, [FromServices]IProjectEnvironmentsService service)
        {
            return await service.CreateOrUpdateAsync(Environment);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<object> Delete(string id, [FromServices]IProjectEnvironmentsService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }
    }
}