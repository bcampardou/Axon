using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Services
{
    public interface ITenantsService : IService<Tenant, TenantDTO>
    {
        Task<Tenant> FindOneByName(string name);
    }
}
