using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedPoint.Exceptions;
using RedPoint.Exceptions.Security;
using RedPoint.Services;
using RedPoint.Services.Security;

namespace RedPoint.Areas.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRequestValidator _requestValidator;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IAccountRequestValidator requestValidator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _requestValidator = requestValidator;
        }
        
        public async Task<object> Login(UserLoginDto model)
        {
            ValidateLoginRequest(model);
            
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                return await GenerateJwtToken(model.Username, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }
        
        private void ValidateLoginRequest(UserLoginDto model)
        {
            try
            {
                _requestValidator.IsLoginRequestValid(model);
            }
            catch (RequestInvalidException e)
            {
                throw new RequestInvalidException("Login request invalid", e);
            }
        }
        
        public async Task<object> Register(UserRegisterDto model)
        {
            ValidateRegisterRequest(model);
            
            var user = new IdentityUser
            {
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await GenerateJwtToken(model.Username, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }
        
        private void ValidateRegisterRequest(UserRegisterDto model)
        {
            try
            {
                _requestValidator.IsRegisterRequestValid(model);
            }
            catch (RequestInvalidException e)
            {
                throw new RequestInvalidException("Register request invalid", e);
            }
        }
        
        private async Task<object> GenerateJwtToken(string username, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}