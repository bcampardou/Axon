using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Data.Abstractions.Entities;
using EasyCaching.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Axon.Business.Services
{
    public class RolesService : Service, IRolesService
    {
        private RoleManager<Role> _roleManager;

        public RolesService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, RoleManager<Role> roleManager) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateAsync(RoleDTO role)
        {
            return await _roleManager.CreateAsync(_ApplyChanges(role, new Role()));
        }

        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return await _roleManager.DeleteAsync(role);
        }

        public async Task<RoleDTO> FindAsync(Guid id)
        {
            return _ToDTO(await _roleManager.FindByIdAsync(id.ToString()));
        }

        public async Task<RoleDTO> FindByNameAsync(string name)
        {
            return _ToDTO(await _roleManager.FindByNameAsync(name));
        }

        public async Task<IdentityResult> UpdateAsync(RoleDTO dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id.ToString());
            return await _roleManager.UpdateAsync(_ApplyChanges(dto, role));
        }

        protected RoleDTO _ToDTO(Role Role)
        {
            if (Role == null) return null;
            return AdapterFactory.Get<RoleAdapter>().Convert(Role);
        }

        protected Role _ApplyChanges(RoleDTO RoleDTO, Role Role)
        {
            if (RoleDTO == null || Role == null) return null;
            return AdapterFactory.Get<RoleAdapter>().Bind(Role, RoleDTO);
        }

        protected List<RoleDTO> _ToDTO(IEnumerable<Role> entities)
        {
            var results = new List<RoleDTO>();
            var adapter = AdapterFactory.Get<RoleAdapter>();
            foreach (var Role in entities)
            {
                if (Role == null) return null;
                results.Add(adapter.Convert(Role));
            }
            return results;
        }

        protected List<Role> _ApplyChanges(IEnumerable<(RoleDTO RoleDTO, Role Role)> datas)
        {
            var results = new List<Role>();
            var adapter = AdapterFactory.Get<RoleAdapter>();
            foreach (var (RoleDTO, Role) in datas)
            {
                if (RoleDTO == null || Role == null) continue;
                results.Add(
                    adapter.Bind(Role, RoleDTO)
                );
            }
            return results;
        }
    }
}
