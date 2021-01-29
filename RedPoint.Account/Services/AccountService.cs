using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(
            UserManager<IdentityUser> userManager,
            IAccountRequestValidator requestValidator,
            SignInManager<IdentityUser> signInManager,
            ITokenGenerator tokenGenerator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _requestValidator = requestValidator;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> Login(UserLoginDto model)
        {
            await _requestValidator.IsLoginRequestValid(model);
            await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            var appUser = _userManager.Users.SingleOrDefault(u => u.UserName == model.Username);
            return _tokenGenerator.GenerateToken(model.Username, appUser);
        }

        public async Task<string> Register(UserRegisterDto model)
        {
            await _requestValidator.IsRegisterRequestValid(model);
            await _userManager.CreateAsync(new IdentityUser(model.Username), model.Password);
            
            var appUser = _userManager.Users.SingleOrDefault(u => u.UserName == model.Username);
            return _tokenGenerator.GenerateToken(model.Username, appUser);
        }

        public Task Update(UserUpdateDto model)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(UserLoginDto model)
        {
            await _requestValidator.IsLoginRequestValid(model);

            var user = await _userManager.FindByNameAsync(model.Username);
            await _userManager.DeleteAsync(user);
        }

    }
}