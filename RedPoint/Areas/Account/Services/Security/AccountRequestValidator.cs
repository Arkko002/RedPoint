using System;
using System.Collections.Generic;
using System.IO;
using RedPoint.Areas.Account.Models;
using RedPoint.Exceptions.Security;

namespace RedPoint.Areas.Account.Services.Security
{
    public class AccountRequestValidator : IAccountRequestValidator
    {
        public AccountError IsLoginRequestValid(UserLoginDto requestDto)
        {
            throw new NotImplementedException();
        }

        public AccountError IsRegisterRequestValid(UserRegisterDto requestDto)
        {
            var passwordFile = File.ReadAllLines("test.txt"); // TODO Add configurable path to file
            var passwordList = new List<string>(passwordFile);

            if (passwordList.Contains(requestDto.Password))
            {
                return new AccountError(AccountErrorType.PasswordTooWeak);
            }
            
            return new AccountError(AccountErrorType.NoError);
        }
    }
}