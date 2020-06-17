using System;
using System.Collections.Generic;
using System.Text;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Business.Abstractions.Services
{
    public interface IInterventionsService<T, ST, DTO> : IService<T, DTO>
        where ST : IdentifiedEntity
        where T: Intervention<ST>, new()
        where DTO : InterventionDTO<T>, new()
    {
    }
}
