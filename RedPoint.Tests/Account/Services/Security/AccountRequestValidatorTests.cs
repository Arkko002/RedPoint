using System.Threading;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Models.Errors;
using RedPoint.Account.Services.Security;
using RedPoint.Tests.Mocks;
using Xunit;

namespace RedPoint.Tests.Account.Services.Security
{
    public class AccountRequestValidatorTests
    {
        public AccountRequestValidatorTests()
        {
            var configuration = new MockConfiguration("full.txt");
            var provider = new AccountSecurityConfigurationProvider(configuration);

            //Quick hack to prevent race condition on password file creation
            Thread.Sleep(100);
            _requestValidator = new AccountRequestValidator(provider);
        }

        private readonly AccountRequestValidator _requestValidator;

        [Fact]
        public void ValidRequestForm_ShouldReturnNoErrorType()
        {
            var dto = new UserRegisterDto
            {
                Username = "ValidUsername",
                Password = "SecurePassword"
            };

            var returnError = _requestValidator.IsRegisterRequestValid(dto);

            Assert.True(returnError.ErrorType == AccountErrorType.NoError);
        }

        [Fact]
        public void WeakPassword_ShouldReturnWeakPasswordError()
        {
            var dto = new UserRegisterDto
            {
                Username = "ValidUsername",
                Password = "test"
            };

            var returnError = _requestValidator.IsRegisterRequestValid(dto);

            Assert.True(returnError.ErrorType == AccountErrorType.PasswordTooWeak);
        }
    }
}