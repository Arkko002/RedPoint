using System.Threading.Tasks;
using RedPoint.Account.Models.Account;

namespace RedPoint.Account.Services
{
    /// <summary>
    /// Provided methods necessary for account manipulation.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Attempts to log in user with the provided data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>JWT token on successful login.</returns>
        Task<string> Login(UserLoginDto model);
        
        /// <summary>
        /// Attempts to register user with the provided data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>JWT token on successful register.</returns>
        Task<string> Register(UserRegisterDto model);
        Task<bool> Delete(UserLoginDto model);
    }
}