using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services.Security;
using RedPoint.Exceptions;
using ILogger = NLog.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace RedPoint.Areas.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRequestValidator _requestValidator;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<AccountService> _logger;

        private IdentityUser _user;
            
        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IAccountRequestValidator requestValidator,
            ILogger<AccountService> logger
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _requestValidator = requestValidator;
            _logger = logger;
        }
        
        public void AssignApplicationUser(ClaimsPrincipal user)
        {
            _user = _userManager.GetUserAsync(user).Result;
        }
        
        public async Task<object> Login(UserLoginDto model)
        {
            var validationResult = _requestValidator.IsLoginRequestValid(model);
            if (validationResult.ErrorType != AccountErrorType.NoError)
            {
                HandleAccountError(validationResult);
            }
            
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                return await GenerateJwtToken(model.Username, appUser);
            }

            if (result.IsLockedOut)
            {
                AccountError error = new AccountError(AccountErrorType.UserLockedOut,
                    LogLevel.Warning,
                    $"{_user.Id} was locked out of account.");
                
            }
            
            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }
        

        
        public async Task<object> Register(UserRegisterDto model)
        {
            var validationResult = _requestValidator.IsRegisterRequestValid(model);
            if (validationResult.ErrorType != AccountErrorType.NoError)
            {
                HandleAccountError(validationResult);
            }
            
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
        
        private void HandleAccountError(AccountError accountError)
        {
            if (accountError.LogMessage != null)
            {
                _logger.Log(accountError.LogLevel, accountError.LogMessage);
            }

            switch (accountError.ErrorType)
            {
                case AccountErrorType.NoError:
                    return;

                case AccountErrorType.PasswordTooWeak:
                    throw new InvalidRequestException("Provided password is too weak.");
                
                case AccountErrorType.UserLockedOut:
                    throw new InvalidRequestException("User was locked out of the account.");
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