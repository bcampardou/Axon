using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Models;
using Microsoft.AspNetCore.Identity;

namespace Axon.Business.Abstractions.Services
{
    public interface IRolesService : IService
    {
        Task<IdentityResult> CreateAsync(RoleDTO role);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<RoleDTO> FindAsync(Guid id);
        Task<RoleDTO> FindByNameAsync(string name);
        Task<IdentityResult> UpdateAsync(RoleDTO dto);
    }
}
