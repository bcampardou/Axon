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
    public class NetworksController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<NetworkDTO>> Get([FromServices]INetworksService service)
        {
            return await service.FindAllAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<NetworkDTO> Get(Guid id, [FromServices]INetworksService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost]
        public async Task<object> Post([FromBody]NetworkDTO Network, [FromServices]INetworksService service)
        {
            return await service.CreateOrUpdateAsync(Network);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<object> Delete(Guid id, [FromServices]INetworksService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }
    }
}