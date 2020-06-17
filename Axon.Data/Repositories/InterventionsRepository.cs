using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities.Base;
using Axon.Data.Abstractions.Repositories;

namespace Axon.Data.Repositories
{
    public abstract class InterventionsRepository<T, TSUB> : RepositoryWithIdentifier<T>, IInterventionsRepository<T, TSUB>
        where T : Intervention<TSUB>, new()
        where TSUB: IdentifiedEntity
    {
        public InterventionsRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}
