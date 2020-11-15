using System.Collections.Generic;

namespace RedPoint.Account.Services.Security
{
    /// <summary>
    /// Loads external config files necessary for validating user's account requests.
    /// </summary>
    public interface IAccountSecurityConfigurationProvider
    {
        List<string> GetBlacklistedPasswords();
    }
}