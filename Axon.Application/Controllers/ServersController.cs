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
    public class ServersController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ServerDTO>> Get([FromServices]IServersService service)
        {
            return await service.FindAllAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ServerDTO> Get(Guid id, [FromServices]IServersService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost]
        public async Task<object> Post([FromBody]ServerDTO server, [FromServices]IServersService service)
        {
            return await service.CreateOrUpdateAsync(server);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<object> Delete(Guid id, [FromServices]IServersService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }
    }
}