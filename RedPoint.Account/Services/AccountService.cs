using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
using RedPoint.Account.Services.Security;
using RedPoint.Account.Exceptions;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Models.Errors;

namespace RedPoint.Account.Services
{
    //TODO Add Update, Delete actions
    /// <inheritdoc/>
    public class AccountService : IAccountService
    {
        private readonly IAccountRequestValidator _requestValidator;
        private readonly IAccountErrorHandler _errorHandler;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IAccountRequestValidator requestValidator,
            IAccountErrorHandler errorHandler,
            ITokenGenerator tokenGenerator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _requestValidator = requestValidator;
            _tokenGenerator = tokenGenerator;
            _errorHandler = errorHandler;
        }

        public async Task<string> Login(UserLoginDto model)
        {
            var validationResult = await _requestValidator.IsLoginRequestValid(model);
            _errorHandler.HandleError(validationResult);
            
            //TODO Handle login with password hash, not username comparision
            var appUser = _userManager.Users.SingleOrDefault(u => u.UserName == model.Username);
            return _tokenGenerator.GenerateToken(model.Username, appUser);
        }

        public async Task<string> Register(UserRegisterDto model)
        {
            var validationResult = await _requestValidator.IsRegisterRequestValid(model);
            _errorHandler.HandleError(validationResult);

            //TODO Handle login with password hash, not username comparision
            var appUser = _userManager.Users.SingleOrDefault(u => u.UserName == model.Username);
            return _tokenGenerator.GenerateToken(model.Username, appUser);
        }

        //TODO
        public Task<bool> Delete(UserLoginDto model)
        {
            throw new NotImplementedException();
        }
    }
}