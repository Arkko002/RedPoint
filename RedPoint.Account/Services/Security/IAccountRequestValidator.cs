using RedPoint.Account.Models;
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
        /// <param name="requestDto"></param>
        /// <returns>AccountError object with error details. AccountErrorType is set to NoError on valid request.</returns>
        AccountError IsRegisterRequestValid(UserRegisterDto requestDto);
        
        /// <summary>
        /// Performs internal check of user's login request.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns>AccountError object with error details. AccountErrorType is set to NoError on valid request.</returns>
        AccountError IsLoginRequestValid(UserLoginDto requestDto);
    }
}