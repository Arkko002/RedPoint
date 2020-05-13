using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services;
using RedPoint.Areas.Account.Services.Security;
using RedPoint.Exceptions;
using Xunit;


namespace RedPoint.Tests.Account
{
    public class AccountServiceTests
    {
        private AccountService _service;
        private Mock<MockUserManager> _userManager;
        private Mock<MockSignInManager> _signInManager;
        private Mock<IAccountRequestValidator> _requestValidator;

        public AccountServiceTests()
        {
            var configuration = new MockConfiguration("none");
            configuration["JwtKey"] = "testKey-LongEnoughForHS256";
            configuration["JwtExpireDays"] = "1";
            configuration["JwtIssuer"] = "testIssuer";

            var users = new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = "1",
                    UserName = "Username"
                }
            }.AsQueryable();

            _userManager = new Mock<MockUserManager>();
            _userManager.Setup(x => x.Users).Returns(users);
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _signInManager = new Mock<MockSignInManager>();
            
            _requestValidator = new Mock<IAccountRequestValidator>();
            var tokenGenerator = new Mock<JwtTokenGenerator>(configuration);
            
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            _requestValidator.Setup(x => x.IsLoginRequestValid(It.IsAny<UserLoginDto>()))
                .Returns(new AccountError(AccountErrorType.NoError));
            _requestValidator.Setup(x => x.IsRegisterRequestValid(It.IsAny<UserRegisterDto>()))
                .Returns(new AccountError(AccountErrorType.NoError));

            _service = new AccountService(_userManager.Object,
                _signInManager.Object,
                _requestValidator.Object,
                new NullLogger<AccountService>(),
                tokenGenerator.Object);
        }

        [Fact]
        public void Login_ValidAttempt_ShouldReturnJwtToken()
        {
            var returnValue = _service.Login(new UserLoginDto {Password = "Password", Username = "Username"}).Result;

            Assert.IsType<string>(returnValue);
        }
        
        [Fact]
        public void Login_ValidationError_ShouldThrowException()
        {
            _requestValidator.Setup(x => x.IsLoginRequestValid(It.IsAny<UserLoginDto>()))
                .Returns(new AccountError(AccountErrorType.LoginFailure));

            Assert.ThrowsAsync<InvalidRequestException>(() => _service.Login(new UserLoginDto()));
        }
        
        [Fact]
        public void Login_SignInManagerError_ShouldThrowException()
        {
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);
            
            Assert.ThrowsAsync<InvalidRequestException>(() => _service.Login(new UserLoginDto { Username = "Username", Password = "Password"}));
        }

        [Fact]
        public void Login_AccountLockedOut_ShouldThrowException()
        {
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.LockedOut);
            
            Assert.ThrowsAsync<InvalidRequestException>(() => _service.Login(new UserLoginDto{ Username = "Username", Password = "Password"}));
        }

        [Fact]
        public void Register_ValidForm_ShouldReturnJwtToken()
        {
            var returnValue = _service.Register(new UserRegisterDto { Password = "Password", Username = "Username" }).Result;

            Assert.IsType<string>(returnValue);
        }

        [Fact]
        public void Register_ValidationError_ShouldThrowException()
        {
            _requestValidator.Setup(x => x.IsRegisterRequestValid(It.IsAny<UserRegisterDto>()))
                .Returns(new AccountError(AccountErrorType.PasswordTooWeak));

            Assert.ThrowsAsync<InvalidRequestException>(() => _service.Register(new UserRegisterDto { Username = "Username", Password = "Password"}));
        }

        [Fact]
        public void Register_UserManagerError_ShouldThrowException()
        {
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            Assert.ThrowsAsync<InvalidRequestException>(() => _service.Register(new UserRegisterDto { Username = "Username", Password = "Password"}));
        }
    }
}