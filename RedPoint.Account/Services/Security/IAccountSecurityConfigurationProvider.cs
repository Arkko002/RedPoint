using System.Collections.Generic;

namespace RedPoint.Areas.Account.Services.Security
{
    public interface IAccountSecurityConfigurationProvider
    {
        List<string> GetBlacklistedPasswords();
    }
}