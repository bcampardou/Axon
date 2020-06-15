using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities.Base;
using Axon.Data.Abstractions.Repositories;

namespace Axon.Data.Repositories
{
    public abstract class TeammatesRepository<ENTITY, T> : Repository<ENTITY>, ITeammatesRepository<ENTITY, T>
        where ENTITY : TeamMate<T>, new()
        where T: IdentifiedEntity
    {
        public TeammatesRepository(AxonDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}
