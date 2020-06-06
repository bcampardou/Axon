using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Core.Exceptions;
using Axon.Core.Guards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Axon.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IUsersService _userService;
        private readonly IConfiguration configuration;
        private static readonly HttpClient Client = new HttpClient();

        public UsersController(
            IUsersService userService,
            IConfiguration config)
        {
            _userService = userService;
            configuration = config;
        }

        [HttpGet]
        [Route("refreshtoken")]
        public async Task<object> RefreshToken()
        {
            var user = await _userService.FindByUserNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                );
            return new
            {
                user,
                token = GenerateJwtToken(user)
            };

        }

        [HttpPost("signin")]
        public async Task<object> SignInAsync([FromBody] LoginDTO model)
        {
            var isEmail = Ensure.Arguments.IsValidEmail(model.Login);

            var result = isEmail ?
                        await _userService.EmailSignInAsync(model.Login, model.Password) :
                        await _userService.UserNameSignInAsync(model.Login, model.Password);

            if (result.Succeeded)
            {
                var user = isEmail ? await _userService.FindByEmailAsync(model.Login) :
                                    await _userService.FindByUserNameAsync(model.Login);
                return new
                {
                    user,
                    token = GenerateJwtToken(user)
                };
            }

            throw new AxonException("INVALID_LOGIN_ATTEMPT");
        }

        [Authorize]
        [HttpGet("signout")]
        public async Task SignOutAsync()
        {
            await _userService.SignOutAsync();
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string token, [FromServices]IUsersService serv)
        {
            var result = await serv.ConfirmEmail(userId, token);


            return this.View("~/Views/Users/ConfirmEmail.cshtml", new BooleanResponseModel { Success = result.Succeeded, Url = $"{configuration.GetValue<string>("Application:URL")}" });
        }

        [HttpGet("sendConfirmationEmail/{email}")]
        public async Task SendConfirmationEmail(string email, [FromServices] IUsersService service)
        {
            await service.GenerateAndSendConfirmationEmail(email, $"{configuration.GetValue<string>("Application:URL")}/api/users/confirm");
        }

        [HttpPost("register")]
        public async Task<object> RegisterAsync([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid) throw new AxonException("Invalid registration", ModelState.SelectMany(e => e.Value.Errors.Select(es => es.ErrorMessage)));
            var user = new UserDTO
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userService.CreateAsync(user, model.Password, $"{configuration.GetValue<string>("Application:URL")}/api/users/confirm");

            if (result.Succeeded)
            {
                return await _userService.FindByEmailAsync(model.Email);
            }

            throw new AxonException("Invalid registration", result.Errors.Select(e => e.Description));
        }

        [HttpGet("{id}")]
        public async Task<UserLightDTO> Get(string id, [FromServices]IUsersService usersService)
        {
            return await usersService.FindAsync(id) as UserLightDTO;
        }

        [HttpGet("")]
        public async Task<List<UserDTO>> Get([FromServices]IUsersService usersService)
        {
            return await usersService.FindAllAsync();
        }



        [HttpPost()]
        public async Task<UserDTO> Post(UserDTO user, [FromServices]IUsersService usersService)
        {
            Ensure.Arguments.ThrowIfNull(user, nameof(user));
            Ensure.Arguments.ThrowIfNotValidGuid(user.Id, nameof(user.Id));
            await usersService.UpdateAsync(user);
            return await usersService.FindAsync(user.Id);
        }

        private object GenerateJwtToken(UserDTO user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetValue<String>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(this.configuration.GetValue<int>("Tokens:Lifetime")),
                audience: this.configuration.GetValue<String>("Tokens:Audience"),
                issuer: this.configuration.GetValue<String>("Tokens:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public class LoginDTO
        {
            [Required]
            public string Login { get; set; }

            [Required]
            public string Password { get; set; }

        }

        public class RegisterDTO
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }


            public string PhoneNumber { get; set; }
        }

        public class BooleanResponseModel
        {
            public bool Success { get; set; }
            public string Url { get; set; }
        }
    }
}