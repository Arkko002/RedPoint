using System;
using RedPoint.Account.Models;

namespace RedPoint.Account.Services.Security
{
    /// <inheritdoc/> 
    public class AccountRequestValidator : IAccountRequestValidator
    {
        private readonly IAccountSecurityConfigurationProvider _provider;

        public AccountRequestValidator(IAccountSecurityConfigurationProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc/>
        public AccountError IsLoginRequestValid(UserLoginDto requestDto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public AccountError IsRegisterRequestValid(UserRegisterDto requestDto)
        {
            var passwordList = _provider.GetBlacklistedPasswords();

            if (passwordList.Contains(requestDto.Password))
            {
                return new AccountError(AccountErrorType.PasswordTooWeak);
            }

            return new AccountError(AccountErrorType.NoError);
        }
    }
}