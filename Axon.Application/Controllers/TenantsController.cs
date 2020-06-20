using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Core.Guards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Axon.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<TenantDTO>> Get([FromServices]ITenantsService service)
        {
            return await service.FindAllAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<TenantDTO> Get(Guid id, [FromServices]ITenantsService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost]
        public async Task<object> Post([FromBody]TenantDTO Environment, [FromServices]ITenantsService service)
        {
            return await service.CreateOrUpdateAsync(Environment);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<object> Delete(Guid id, [FromServices]ITenantsService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }
    }
}