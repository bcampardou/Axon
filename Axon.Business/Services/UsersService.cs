using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Adapters.Factory;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Models.Authentication;
using Axon.Business.Abstractions.Services;
using Axon.Business.Rules;
using Axon.Core.Configurations;
using Axon.Core.Constants;
using Axon.Core.Exceptions;
using Axon.Core.Guards;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using EasyCaching.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axon.Business.Services
{
    public class UsersService : Service, IUsersService
    {
        protected UserManager<User> _userManager;
        protected SignInManager<User> _signInManager;
        protected Lazy<IUsersRepository> _lazyRepository;
        protected IEmailService _emailService;
        protected IUsersRepository _repository { get { return _lazyRepository.Value; } }
        public UsersService(IEmailService emailService, ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, Lazy<IUsersRepository> repo) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider)
        {
            _userManager = _serviceProvider.GetRequiredService<UserManager<User>>();
            _signInManager = _serviceProvider.GetRequiredService<SignInManager<User>>();
            _lazyRepository = repo;
            _emailService = emailService;
        }

        public async Task<List<UserDTO>> FindAllAsync(bool useCache = true, int? maximumRows = null, int skipRows = 0)
        {
            if (!useCache)
                return await _findAll(maximumRows, skipRows);

            var cacheKey = BusinessRules.CacheListKey(typeof(User));
            var cached = await _cachingProvider.GetAsync<List<UserDTO>>(cacheKey);
            if (cached.HasValue && !cached.IsNull)
            {
                return cached.Value;
            }
            var results = await _findAll(maximumRows, skipRows);
            await _cachingProvider.SetAsync(cacheKey, results, TimeSpan.FromSeconds(_configuration.GetValue<int>(ConfigurationConstants.CacheInSeconds)));
            return results;
        }

        private async Task<List<UserDTO>> _findAll(int? maximumRows = null, int skipRows = 0)
        {
            var results = await _repository.FindAllAsync(maximumRows, skipRows);
            return results.Select(e => _ToDTO(e)).ToList();

        }

        public async Task<SignInResult> EmailSignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) { throw new AxonException("No account found for this email/username."); }
            if (!user.EmailConfirmed) { throw new AxonException("Email address is not confirmed. Please check your emails."); }
            if (!user.IsActive) { throw new AxonException("Your account has to be validated by your company administrator."); }

            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<SignInResult> UserNameSignInAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) { throw new AxonException("No account found for this email/username."); }
            if (!user.EmailConfirmed) { throw new AxonException("Email address is not confirmed. Please check your emails."); }
            if (!user.IsActive) { throw new AxonException("Your account has to be validated by your company administrator."); }

            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public bool IsSignedIn()
        {
            return _signInManager.IsSignedIn(_currentUser);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> CreateAsync(UserDTO user, string password, string url)
        {
            var entity = _ApplyChanges(user, new User()
            {
                SecurityStamp = ""
            });
            entity.IsActive = false;

            var result = await _userManager.CreateAsync(entity, password);

            if (result.Succeeded)
            {
                //await GenerateAndSendConfirmationEmail(entity.NormalizedEmail, url);
            }
            else if (result.Errors.Any())
            {
                var error = result.Errors.FirstOrDefault();
                throw new AxonException("Invalid registration", result.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task GenerateAndSendConfirmationEmail(string email, string url = "")
        {
            email = email.Trim().ToUpper();
            var user = await _repository.FindOneByPredicateAsync(u => u.NormalizedEmail == email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = $"{url}?userId={user.Id}&token={UrlEncoder.Default.Encode(code)}";
            await _emailService.SendMailWithData(user.UserName, user.Email, "Axon - Email confirmation", "ActivateAccount.cshtml", new ActivateAccountModel { Username = user.UserName, CallbackUrl = callbackUrl });
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string token)
        {
            var user = await _repository.FindAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<IdentityResult> CreateOrUpdateAsync(UserDTO user, string password = null)
        {
            var entity = _ApplyChanges(user, new User());
            if (Ensure.Arguments.IsValidGuid(user.Id))
            {
                password = Ensure.Arguments.IsStringEmpty(password) ? BusinessRules.Users.GenerateRandomPassword(IdentityConfiguration.PasswordOpts) : password;

                return await _userManager.CreateAsync(entity, password);
            }
            return await _userManager.UpdateAsync(entity);
        }

        public async Task<IdentityResult> UpdateAsync(UserDTO dto)
        {
            var entity = await _userManager.FindByIdAsync(dto.Id);
            entity = _ApplyChanges(dto, entity);
            var result = await _userManager.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
            return result;
        }

        public async Task<IdentityResult> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await _userManager.DeleteAsync(user);
        }

        public async Task<UserDTO> FindAsync(string id)
        {
            return _ToDTO(await _repository.FindAsync(id));
        }

        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            email = email.Trim().ToUpper();
            return _ToDTO(await _repository.FindOneByPredicateAsync(u => u.NormalizedEmail == email));
            //await _userManager.FindByEmailAsync(email)
        }

        public async Task<UserDTO> FindByUserNameAsync(string name)
        {
            name = name.Trim().ToUpper();
            return _ToDTO(await _repository.FindOneByPredicateAsync(u => u.NormalizedUserName == name));
            //await _userManager.FindByNameAsync(name)
        }

        protected UserDTO _ToDTO(User User)
        {
            if (User == null) return null;
            return AdapterFactory.Get<UserAdapter>().Convert(User);
        }

        protected User _ApplyChanges(UserDTO userDTO, User user)
        {
            if (userDTO == null || user == null) return null;
            return AdapterFactory.Get<UserAdapter>().Bind(user, userDTO);
        }

        protected List<UserDTO> _ToDTO(IEnumerable<User> entities)
        {
            var results = new List<UserDTO>();
            var adapter = AdapterFactory.Get<UserAdapter>();
            foreach (var User in entities)
            {
                if (User == null) return null;
                results.Add(adapter.Convert(User));
            }
            return results;
        }

        protected List<User> _ApplyChanges(IEnumerable<(UserDTO UserDTO, User User)> datas)
        {
            var results = new List<User>();
            var adapter = AdapterFactory.Get<UserAdapter>();
            foreach (var (UserDTO, User) in datas)
            {
                if (UserDTO == null || User == null) continue;
                results.Add(
                    adapter.Bind(User, UserDTO)
                );
            }
            return results;
        }
    }
}
