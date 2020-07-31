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
    public class KnowledgeSheetsController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<KnowledgeSheetDTO>> Get([FromServices]IKnowledgeSheetsService service)
        {
            return await service.FindAllAsync();
        }

        [HttpGet("base")]
        public async Task<IEnumerable<KnowledgeSheetDTO>> GetBase([FromServices]IKnowledgeSheetsService service)
        {
            return await service.FindRootAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<KnowledgeSheetDTO> Get(Guid id, [FromServices]IKnowledgeSheetsService service)
        {
            return await service.FindAsync(id);
        }

        [HttpPost]
        public async Task<object> Post([FromBody]KnowledgeSheetDTO server, [FromServices]IKnowledgeSheetsService service)
        {
            return await service.CreateOrUpdateAsync(server);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<object> Delete(Guid id, [FromServices]IKnowledgeSheetsService service)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            return await service.DeleteAsync(await service.FindAsync(id));
        }
    }
}