using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Data.Abstractions.Repositories
{
    public interface INetworkTeammatesRepository : ITeammatesRepository<NetworkTeammate, Network>
    {
    }
}
