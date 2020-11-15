using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RedPoint.Account.Services.Security
{
    /// <inheritdoc/>
    public class AccountSecurityConfigurationProvider : IAccountSecurityConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public AccountSecurityConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Loads a list of blacklisted passwords from a file specified in IConfiguration interface.
        /// Each password in the file has to be separated with a newline. 
        /// Path to the blacklisted password file can be set in appsettings.json by changing "BlacklistedPasswords" field.
        /// </summary>
        /// <returns>List of strings where each string is a password from the file.</returns>
        public List<string> GetBlacklistedPasswords()
        {
            var passwordFile = File.ReadAllLines(_configuration["BlacklistedPasswords"]);
            var passwordList = new List<string>(passwordFile);

            return passwordList;
        }
    }
}