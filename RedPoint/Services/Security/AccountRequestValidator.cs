using System.Collections.Generic;
using System.IO;

namespace RedPoint.Services.Security
{
    public class AccountRequestValidator : IAccountRequestValidator
    {
        public bool IsLoginRequestValid()
        {
            //TODO Keep track of number of tries for user, log abnormal tries,
            //block user requests completly after too many tries
        }

        public bool IsRegisterRequestValid(UserRegisterDto requestDto)
        {
            var passwordFile = File.ReadAllLines("test.txt"); // TODO Add configurable path to file
            var passwordList = new List<string>(passwordFile);

            if (passwordList.Contains(requestDto.Password))
            {
                return false;
            }

            return true;
        }
    }
}