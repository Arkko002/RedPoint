using System.Collections.Generic;

namespace RedPoint.Account.Services.Security
{
    public interface IAccountSecurityConfigurationProvider
    {
        List<string> GetBlacklistedPasswords();
    }
}