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
    public class InterventionsController : ControllerBase
    {
        [HttpGet("project")]
        public async Task<IEnumerable<ProjectInterventionDTO>> Get([FromServices]IProjectInterventionsService service)
        {
            return await service.FindAllAsync();
        }

        [Route("{id}")]
        [HttpGet("project")]
        public async Task<ProjectInterventionDTO> Get(Guid id, [FromServices]IProjectInterventionsService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost("project")]
        public async Task<object> Post([FromBody]ProjectInterventionDTO intervention, [FromServices]IProjectInterventionsService service)
        {
            return await service.CreateOrUpdateAsync(intervention);
        }

        [Route("{id}")]
        [HttpDelete("project")]
        public async Task<object> Delete(Guid id, [FromServices]IProjectInterventionsService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }

        [HttpGet("network")]
        public async Task<IEnumerable<NetworkInterventionDTO>> Get([FromServices]INetworkInterventionsService service)
        {
            return await service.FindAllAsync();
        }

        [Route("{id}")]
        [HttpGet("network")]
        public async Task<NetworkInterventionDTO> Get(Guid id, [FromServices]INetworkInterventionsService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost("network")]
        public async Task<object> Post([FromBody]NetworkInterventionDTO intervention, [FromServices]INetworkInterventionsService service)
        {
            return await service.CreateOrUpdateAsync(intervention);
        }

        [Route("{id}")]
        [HttpDelete("network")]
        public async Task<object> Delete(Guid id, [FromServices]INetworkInterventionsService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }

        [HttpGet("server")]
        public async Task<IEnumerable<ServerInterventionDTO>> Get([FromServices]IServerInterventionsService service)
        {
            return await service.FindAllAsync();
        }

        [Route("{id}")]
        [HttpGet("server")]
        public async Task<ServerInterventionDTO> Get(Guid id, [FromServices]IServerInterventionsService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost("server")]
        public async Task<object> Post([FromBody]ServerInterventionDTO intervention, [FromServices]IServerInterventionsService service)
        {
            return await service.CreateOrUpdateAsync(intervention);
        }

        [Route("{id}")]
        [HttpDelete("server")]
        public async Task<object> Delete(Guid id, [FromServices]IServerInterventionsService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }
    }
}