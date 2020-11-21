using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Moq;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Models.Errors;
using RedPoint.Account.Services.Security;
using RedPoint.Tests.Mocks;
using Xunit;

namespace RedPoint.Tests.Account.Services.Security
{
    public class AccountRequestValidatorTests
    {
        private readonly Mock<MockUserManager<IdentityUser>> _userManager;
        private readonly Mock<MockSignInManager<IdentityUser>> _signInManager;
        
        public AccountRequestValidatorTests()
        {
            var configuration = new MockConfiguration("full.txt");
            var provider = new AccountSecurityConfigurationProvider(configuration);


            var users = new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = "1",
                    UserName = "Username"
                }
            }.AsQueryable();

            _userManager = new Mock<MockUserManager<IdentityUser>>();
            _userManager.Setup(x => x.Users).Returns(users);
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _signInManager = new Mock<MockSignInManager<IdentityUser>>();
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            //Quick hack to prevent race condition on password file creation
            Thread.Sleep(100);
            _requestValidator = new AccountRequestValidator(provider, _signInManager.Object, _userManager.Object);
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

            var returnError = _requestValidator.IsRegisterRequestValid(dto).Result;

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

            var returnError = _requestValidator.IsRegisterRequestValid(dto).Result;

            Assert.True(returnError.ErrorType == AccountErrorType.PasswordTooWeak);
        }
    }
}