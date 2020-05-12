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
        private readonly ITokenGenerator _tokenGenerator;
        
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        private readonly ILogger<AccountService> _logger;

        private IdentityUser _user;
            
        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IAccountRequestValidator requestValidator,
            ILogger<AccountService> logger,
            ITokenGenerator tokenGenerator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _requestValidator = requestValidator;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
        }
        
        public void AssignApplicationUser(ClaimsPrincipal user)
        {
            _user = _userManager.GetUserAsync(user).Result;
        }
        
        public async Task<string> Login(UserLoginDto model)
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
                return _tokenGenerator.GenerateToken(model.Username, appUser);
            }
            
            if (result.IsLockedOut)
            {
                AccountError error = new AccountError(AccountErrorType.UserLockedOut,
                    LogLevel.Warning,
                    $"{_user.Id} was locked out of account.");
                HandleAccountError(error);
            }
            
            AccountError loginFailure = new AccountError(AccountErrorType.LoginFailure);
            HandleAccountError(loginFailure);

            //Will never return null, as HandleAccountError always throws an exception on login failure
            return null;
        }
        

        
        public async Task<string> Register(UserRegisterDto model)
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
                return _tokenGenerator.GenerateToken(model.Username, user);
            }

            AccountError unknownError = new AccountError(AccountErrorType.RegisterFailure);
            HandleAccountError(unknownError);

            //Will never return null, as HandleAccountError always throws an exception on register failure
            return null;
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
                
                case AccountErrorType.LoginFailure:
                    throw new InvalidRequestException("The provided credentials were incorrect");
                
                case AccountErrorType.RegisterFailure:
                    throw new InvalidRequestException("An error occured during registration process.");
                
                default:
                    throw new InvalidRequestException("Default claus Error in HandleAccountError switch statement");
            }
        }
    }
}