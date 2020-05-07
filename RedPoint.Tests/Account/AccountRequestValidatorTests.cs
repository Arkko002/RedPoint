using System.Threading;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services.Security;
using Xunit;

namespace RedPoint.Tests.Account
{
    public class AccountRequestValidatorTests
    {
        private AccountRequestValidator _requestValidator;
        
        public AccountRequestValidatorTests()
        {
            MockConfiguration configuration = new MockConfiguration("full.txt");
            AccountSecurityConfigurationProvider provider = new AccountSecurityConfigurationProvider(configuration);
            
            //Quick hack to prevent race condition on password file creation
            Thread.Sleep(100);
            _requestValidator = new AccountRequestValidator(provider);
        }

        [Fact]
        public void ValidRequestForm_ShouldReturnNoErrorType()
        {
            UserRegisterDto dto = new UserRegisterDto()
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
            UserRegisterDto dto = new UserRegisterDto()
            {
                Username = "ValidUsername",
                Password = "test"
            };

            var returnError = _requestValidator.IsRegisterRequestValid(dto);
            
            Assert.True(returnError.ErrorType == AccountErrorType.PasswordTooWeak);
        }
    }
}