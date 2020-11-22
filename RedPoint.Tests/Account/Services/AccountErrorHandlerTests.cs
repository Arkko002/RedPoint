using Moq;
using NLog;
using RedPoint.Account.Exceptions;
using RedPoint.Account.Models.Errors;
using RedPoint.Account.Services;
using Xunit;

namespace RedPoint.Tests.Account.Services
{
    //TODO Verify logging
    public class AccountErrorHandlerTests
    {
        private readonly Mock<AccountErrorHandler> _errorHandler;
    
        public AccountErrorHandlerTests()
        {
            _errorHandler = new Mock<AccountErrorHandler>(new NullLogger(new LogFactory()));
        }

        [Theory]
        [InlineData(AccountErrorType.RegisterFailure)]
        [InlineData(AccountErrorType.PasswordTooWeak)]
        public void HandleError_CreationError_ShouldThrowCreationException(AccountErrorType errorType)
        {
            var error = new AccountError(errorType);

            Assert.Throws<AccountCreationException>(() => _errorHandler.Object.HandleError(error));
        }

        [Fact]
        public void HandleError_LockedOut_ShouldThrowLockOutException()
        {
            var error = new AccountError(AccountErrorType.UserLockedOut);

            Assert.Throws<LockOutException>(() => _errorHandler.Object.HandleError(error));
        }

        [Fact]
        public void HandleError_LoginFailure_ShouldThrowAuthorizationException()
        {
            var error = new AccountError(AccountErrorType.LoginFailure);

            Assert.Throws<AuthorizationAttemptException>(() => _errorHandler.Object.HandleError(error));
        }
    }
}