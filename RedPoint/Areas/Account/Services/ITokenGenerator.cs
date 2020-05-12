using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RedPoint.Areas.Account.Services
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string username, IdentityUser user);
    }
}