using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using NLog;
using RedPoint.Account.Models;
using RedPoint.Account.Services.Security;
using RedPoint.Account.Exceptions;

namespace RedPoint.Account.Services
{
    //TODO Add Update, Delete actions
    public class AccountService : IAccountService
    {
        private readonly Logger _logger;
        private readonly IAccountRequestValidator _requestValidator;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IAccountRequestValidator requestValidator,
            Logger logger,
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

                var error = new AccountError(AccountErrorType.UserLockedOut,
                    LogLevel.Warn,
                    $"{appUser.Id} was locked out of account.");
                HandleAccountError(error);
            }

            var loginFailure = new AccountError(AccountErrorType.LoginFailure);
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
            var registerError = new AccountError(AccountErrorType.RegisterFailure);
            HandleAccountError(registerError);

            //Will never return null, as HandleAccountError always throws an exception on register failure
            return null;
        }

        //TODO
        public Task<bool> Delete(UserLoginDto model)
        {
            throw new NotImplementedException();
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
                    throw new AccountRequestException("Provided password is too weak.");

                case AccountErrorType.UserLockedOut:
                    throw new AccountRequestException("User was locked out of the account.");

                case AccountErrorType.LoginFailure:
                    throw new AccountRequestException("The provided credentials were incorrect");

                case AccountErrorType.RegisterFailure:
                    throw new AccountRequestException("An error occured during registration process.");

                default:
                    // TODO
                    throw new AccountRequestException("Default claus Error in HandleAccountError switch statement");
            }
        }
    }
}