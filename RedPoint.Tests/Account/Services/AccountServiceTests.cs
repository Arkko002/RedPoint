using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NLog;
using NLog.Config;
using RedPoint.Account.Models;
using RedPoint.Account.Services;
using RedPoint.Account.Services.Security;
using RedPoint.Account.Exceptions;
using RedPoint.Tests.Mocks;
using Xunit;

namespace RedPoint.Tests.Account
{
    public class AccountServiceTests
    {
        public AccountServiceTests()
        {
            var configuration = new MockConfiguration("none")
            {
                ["JwtKey"] = "testKey-LongEnoughForHS256",
                ["JwtExpireDays"] = "1",
                ["JwtIssuer"] = "testIssuer"
            };

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

            
            var configPath = Path.Join(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "nlog.config");
            var logFactory = new LogFactory();
            logFactory.Configuration = new XmlLoggingConfiguration(configPath, logFactory);
            
            _service = new AccountService(_userManager.Object,
                _signInManager.Object,
                _requestValidator.Object,
                logFactory.GetCurrentClassLogger(),
                tokenGenerator.Object);
        }

        private readonly AccountService _service;
        private readonly Mock<MockUserManager<IdentityUser>> _userManager;
        private readonly Mock<MockSignInManager<IdentityUser>> _signInManager;
        private readonly Mock<IAccountRequestValidator> _requestValidator;

        [Fact]
        public void Login_AccountLockedOut_ShouldThrowException()
        {
            _signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.LockedOut);

            Assert.ThrowsAsync<AccountRequestException>(() =>
                _service.Login(new UserLoginDto { Username = "Username", Password = "Password" }));
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

            Assert.ThrowsAsync<AccountRequestException>(() =>
                _service.Login(new UserLoginDto { Username = "Username", Password = "Password" }));
        }

        [Fact]
        public void Login_ValidationError_ShouldThrowException()
        {
            _requestValidator.Setup(x => x.IsLoginRequestValid(It.IsAny<UserLoginDto>()))
                .Returns(new AccountError(AccountErrorType.LoginFailure));

            Assert.ThrowsAsync<AccountRequestException>(() => _service.Login(new UserLoginDto()));
        }

        [Fact]
        public void Login_ValidAttempt_ShouldReturnJwtToken()
        {
            var returnValue = _service.Login(new UserLoginDto { Password = "Password", Username = "Username" }).Result;

            Assert.IsType<string>(returnValue);
        }

        [Fact]
        public void Register_UserManagerError_ShouldThrowException()
        {
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            Assert.ThrowsAsync<AccountRequestException>(() =>
                _service.Register(new UserRegisterDto { Username = "Username", Password = "Password" }));
        }

        [Fact]
        public void Register_ValidationError_ShouldThrowException()
        {
            _requestValidator.Setup(x => x.IsRegisterRequestValid(It.IsAny<UserRegisterDto>()))
                .Returns(new AccountError(AccountErrorType.PasswordTooWeak));

            Assert.ThrowsAsync<AccountRequestException>(() =>
                _service.Register(new UserRegisterDto { Username = "Username", Password = "Password" }));
        }

        [Fact]
        public void Register_ValidForm_ShouldReturnJwtToken()
        {
            var returnValue = _service.Register(new UserRegisterDto { Password = "Password", Username = "Username" })
                .Result;

            Assert.IsType<string>(returnValue);
        }
    }
}