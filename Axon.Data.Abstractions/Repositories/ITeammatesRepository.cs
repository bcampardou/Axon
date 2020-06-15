using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Repositories
{
    public interface ITeammatesRepository<ENT, T> : IRepository<ENT>
        where ENT: TeamMate<T>, new()
        where T: IdentifiedEntity
    {
    }
}
