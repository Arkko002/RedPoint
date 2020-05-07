using System;
using System.Collections.Generic;
using System.IO;
using RedPoint.Areas.Account.Models;
using RedPoint.Exceptions.Security;

namespace RedPoint.Areas.Account.Services.Security
{
    public class AccountRequestValidator : IAccountRequestValidator
    {
        private IAccountSecurityConfigurationProvider _provider;

        public AccountRequestValidator(IAccountSecurityConfigurationProvider provider)
        {
            _provider = provider;
        }
        
        public AccountError IsLoginRequestValid(UserLoginDto requestDto)
        {
            throw new NotImplementedException();
        }

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