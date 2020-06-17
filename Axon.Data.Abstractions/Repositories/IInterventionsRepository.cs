using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Repositories
{
    public interface IInterventionsRepository<T, TSUB> : IRepositoryWithIdentifier<T>
        where T: Intervention<TSUB>, new()
        where TSUB: IdentifiedEntity
    {
    }
}
