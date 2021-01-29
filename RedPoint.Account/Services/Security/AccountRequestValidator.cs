using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RedPoint.Account.Exceptions;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Models.Errors;

namespace RedPoint.Account.Services.Security
{
    /// <inheritdoc/> 
    public class AccountRequestValidator : IAccountRequestValidator
    {
        private readonly IAccountSecurityConfigurationProvider _provider;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserValidator<IdentityUser> _userValidator;

        public AccountRequestValidator(IAccountSecurityConfigurationProvider provider,
        SignInManager<IdentityUser> signInManager,
        UserValidator<IdentityUser> userValidator,
        UserManager<IdentityUser> userManager)
        {
            _provider = provider;
            _signInManager = signInManager;
            _userValidator = userValidator;
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task IsLoginRequestValid(UserLoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (!await _signInManager.CanSignInAsync(user))
            {
                throw new AccountLoginException("User can't sign in.", model.Username);
            } 
            if (!await _signInManager.UserManager.CheckPasswordAsync(user, model.Password))
            {
                throw new AccountLoginException("Provided password was incorrect.", model.Username);
            }
            if (await _signInManager.UserManager.IsLockedOutAsync(user))
            {
                throw new LockOutException("Account was locked out.", model.Username);
            }
        }

        public Task IsUpdateRequestValid(UserUpdateDto model)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task IsRegisterRequestValid(UserRegisterDto model)
        {
            var passwordList = _provider.GetBlacklistedPasswords();

            if (passwordList.Contains(model.Password))
            {
                throw new AccountCreationException("Provided password is too weak.");
            }
            
            var user = new IdentityUser
            {
                UserName = model.Username
            };
            
            var validationResult = await _userValidator.ValidateAsync(_userManager, user);

            if (validationResult.Succeeded)
            {
                return;
            }
            
            throw new AccountCreationException("One or multiple errors occured during account creation",
                validationResult.Errors.Select(x => x.Description));
        }
    }
}