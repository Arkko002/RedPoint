using System.Collections.Generic;
using System.IO;
using RedPoint.Areas.Account.Models;
using RedPoint.Exceptions.Security;

namespace RedPoint.Areas.Account.Services.Security
{
    public class AccountRequestValidator : IAccountRequestValidator
    {
        public void IsLoginRequestValid(UserLoginDto requestDto)
        {
            //TODO Keep track of number of tries for user, log abnormal tries,
            //block user requests completly after too many tries
        }

        public void IsRegisterRequestValid(UserRegisterDto requestDto)
        {
            var passwordFile = File.ReadAllLines("test.txt"); // TODO Add configurable path to file
            var passwordList = new List<string>(passwordFile);

            if (passwordList.Contains(requestDto.Password))
            {
                throw new InvalidPasswordException("Password is too weak");
            }
        }
    }
}