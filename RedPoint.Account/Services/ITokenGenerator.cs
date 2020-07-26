using Microsoft.AspNetCore.Identity;

namespace RedPoint.Account.Services
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string username, IdentityUser user);
    }
}