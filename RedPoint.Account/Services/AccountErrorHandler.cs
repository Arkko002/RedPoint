using System;
using Microsoft.Extensions.Logging;
using RedPoint.Account.Exceptions;
using RedPoint.Account.Models.Errors;

namespace RedPoint.Account.Services
{
    public class AccountErrorHandler : IAccountErrorHandler
    {
        private readonly ILogger<AccountErrorHandler> _logger;

        public AccountErrorHandler(ILogger<AccountErrorHandler> logger)
        {
            _logger = logger;
        }
        
        public void HandleError(AccountError error)
        {
            if (!string.IsNullOrEmpty(error.LogMessage))
            {
                _logger.Log(error.LogLevel, error.LogMessage);
            }

            switch (error.ErrorType)
            {
                //TODO User reference 
                case AccountErrorType.NoError:
                    return;

                case AccountErrorType.PasswordTooWeak:
                    throw new AccountCreationException("Provided password is too weak.");

                case AccountErrorType.UserLockedOut:
                    throw new LockOutException("User was locked out of the account.");

                case AccountErrorType.LoginFailure:
                    throw new AuthorizationAttemptException("The provided credentials were incorrect");

                case AccountErrorType.RegisterFailure:
                    throw new AccountCreationException("An error occured during registration process.");

                default:
                    throw new AccountRequestException($"Unknown error occured. ErrorType: {error.ErrorType}     User: {error}");
            }
        }
    }
}