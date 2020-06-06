using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Models;
using Microsoft.AspNetCore.Identity;

namespace Axon.Business.Abstractions.Services
{
    public interface IUsersService : IService
    {
        Task<SignInResult> EmailSignInAsync(string email, string password);
        Task<SignInResult> UserNameSignInAsync(string userName, string password);
        Task GenerateAndSendConfirmationEmail(string email, string url = "");
        Task<List<UserDTO>> FindAllAsync(bool useCache = true, int? maximumRows = null, int skipRows = 0);
        Task SignOutAsync();
        Task<IdentityResult> CreateAsync(UserDTO user, string password, string callbackUrl);
        Task<IdentityResult> ConfirmEmail(string userId, string token);
        Task<IdentityResult> DeleteAsync(string id);
        Task<UserDTO> FindAsync(string id);
        Task<UserDTO> FindByEmailAsync(string email);
        Task<UserDTO> FindByUserNameAsync(string name);
        Task<IdentityResult> CreateOrUpdateAsync(UserDTO user, string password = null);
        bool IsSignedIn();
        Task<IdentityResult> UpdateAsync(UserDTO dto);
    }
}
