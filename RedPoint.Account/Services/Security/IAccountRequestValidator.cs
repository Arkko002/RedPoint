using System.Threading.Tasks;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Models.Errors;

namespace RedPoint.Account.Services.Security
{
    /// <summary>
    /// Detects potential errors and security issues in user's account-related activity.
    /// </summary>
    public interface IAccountRequestValidator
    {
        /// <summary>
        /// Performs internal check of user's registration request.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AccountError object with error details. AccountErrorType is set to NoError on valid request.</returns>
        Task<AccountError> IsRegisterRequestValid(UserRegisterDto model);
        
        /// <summary>
        /// Performs internal check of user's login request.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns>AccountError object with error details. AccountErrorType is set to NoError on valid request.</returns>
        Task<AccountError> IsLoginRequestValid(UserLoginDto requestDto);
    }
}