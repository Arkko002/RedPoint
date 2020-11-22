using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private const string InsecurePassword = "InsecurePassword";

        private readonly Mock<MockUserManager<IdentityUser>> _userManager;
        private readonly Mock<MockSignInManager<IdentityUser>> _signInManager;
        private readonly Mock<IAccountSecurityConfigurationProvider> _configurationProvider;

        private readonly AccountRequestValidator _requestValidator;

        public AccountRequestValidatorTests()
        {
            _configurationProvider = new Mock<IAccountSecurityConfigurationProvider>();
            _configurationProvider.Setup(x => x.GetBlacklistedPasswords())
                .Returns(new List<string> {InsecurePassword});

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

            _requestValidator = new AccountRequestValidator(_configurationProvider.Object, _signInManager.Object,
                _userManager.Object);
        }


        [Fact]
        public void LoginRequest_ValidLoginRequest_ShouldReturnNoErrorType()
        {
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            var loginDto = new UserLoginDto
            {
                Username = "name",
                Password = "password"
            };

            var result = _requestValidator.IsLoginRequestValid(new UserLoginDto()).Result;

            Assert.True(result.ErrorType == AccountErrorType.NoError);
        }

        [Fact]
        public void LoginRequest_LockedOut_ShouldReturnLockedOutErrorType()
        {
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.LockedOut);
            
            var loginDto = new UserLoginDto
            {
                Username = "Username",
                Password = "password"
            };

            var result = _requestValidator.IsLoginRequestValid(loginDto).Result;

            Assert.True(result.ErrorType == AccountErrorType.UserLockedOut);
        }

        [Fact]
        public void LoginRequest_Failure_ShouldReturnLoginFailureType()
        {
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);

            var loginDto = new UserLoginDto
            {
                Username = "name",
                Password = "password"
            };

            var result = _requestValidator.IsLoginRequestValid(loginDto).Result;

            Assert.True(result.ErrorType == AccountErrorType.LoginFailure);
        }

        [Fact]
        public void LoginRequest_NotAllowed_ShouldReturnLoginFailureType()
        {
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.NotAllowed);
                
            var loginDto = new UserLoginDto
            {
                Username = "name",
                Password = "password"
            };

            var result = _requestValidator.IsLoginRequestValid(loginDto).Result;

            Assert.True(result.ErrorType == AccountErrorType.LoginFailure);
        }

        [Fact]
        public void LoginRequest_TwoFactorRequired_ShouldReturnLoginFailureType()
        {
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.TwoFactorRequired);
                
            var loginDto = new UserLoginDto
            {
                Username = "name",
                Password = "password"
            };
            var result = _requestValidator.IsLoginRequestValid(loginDto).Result;

            Assert.True(result.ErrorType == AccountErrorType.LoginFailure);
        }

        [Fact]
        public void RegisterRequest_ValidRequest_ShouldReturnNoErrorType()
        {
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            var user = new UserRegisterDto
            {
                Password = "SecurePassword",
                Username = "Name"
            };

            var error = _requestValidator.IsRegisterRequestValid(user).Result;

            Assert.True(error.ErrorType == AccountErrorType.NoError);
        }

        [Fact]
        public void RegisterRequest_WeakPassword_ShouldReturnWeakPasswordErrorType()
        {
            var user = new UserRegisterDto
            {
                Username = "name",
                Password = InsecurePassword
            };

            var error = _requestValidator.IsRegisterRequestValid(user).Result;

            Assert.True(error.ErrorType == AccountErrorType.PasswordTooWeak);
        }

        [Fact]
        public void RegisterRequest_IdentityErrors_ShouldReturnRegisterFailureErrorType()
        {
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed()));

            var user = new UserRegisterDto
            {
                Password = "SecurePassword",
                Username = "Name"
            };

            var error = _requestValidator.IsRegisterRequestValid(user).Result;

            Assert.True(error.ErrorType == AccountErrorType.RegisterFailure);
        }
    }
}