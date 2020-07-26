using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RedPoint.Account.Services.Security
{
    public class AccountSecurityConfigurationProvider : IAccountSecurityConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public AccountSecurityConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<string> GetBlacklistedPasswords()
        {
            var passwordFile = File.ReadAllLines(_configuration["BlacklistedPasswords"]);
            var passwordList = new List<string>(passwordFile);

            return passwordList;
        }
    }
}