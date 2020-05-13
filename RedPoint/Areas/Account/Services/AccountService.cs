using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services.Security;
using RedPoint.Exceptions;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace RedPoint.Areas.Account.Services
{
    
    //TODO Add Update, Delete actions
    public class AccountService : IAccountService
    {
        private readonly IAccountRequestValidator _requestValidator;
        private readonly ITokenGenerator _tokenGenerator;
        
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        private readonly ILogger<AccountService> _logger;
        
        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IAccountRequestValidator requestValidator,
            ILogger<AccountService> logger,
            ITokenGenerator tokenGenerator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _requestValidator = requestValidator;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
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
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                
                AccountError error = new AccountError(AccountErrorType.UserLockedOut,
                    LogLevel.Warning,
                    $"{appUser.Id} was locked out of account.");
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
            
            //TODO Handle IdentityErrors in result
            AccountError registerError = new AccountError(AccountErrorType.RegisterFailure);
            HandleAccountError(registerError);

            //Will never return null, as HandleAccountError always throws an exception on register failure
            return null;
        }

        //TODO
        public Task<bool> Delete(UserLoginDto model)
        {
            throw new System.NotImplementedException();
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