using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
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

        public AccountRequestValidator(IAccountSecurityConfigurationProvider provider,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
        {
            _provider = provider;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<AccountError> IsLoginRequestValid(UserLoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                return new AccountError(AccountErrorType.NoError);
            }

            if (result.IsLockedOut)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);

                return new AccountError(AccountErrorType.UserLockedOut,
                    LogLevel.Warn,
                    $"{appUser.Id} was locked out of account.",
                    appUser);
            }

            //TODO Other possible errors 
            return new AccountError(AccountErrorType.LoginFailure);
        }

        /// <inheritdoc/>
        public async Task<AccountError> IsRegisterRequestValid(UserRegisterDto model)
        {
            var passwordList = _provider.GetBlacklistedPasswords();

            if (passwordList.Contains(model.Password))
            {
                return new AccountError(AccountErrorType.PasswordTooWeak);
            }
            
            var user = new IdentityUser
            {
                UserName = model.Username
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return new AccountError(AccountErrorType.NoError);
            }
            
            //TODO other possible errors
            return new AccountError(AccountErrorType.RegisterFailure);
        }
    }
}