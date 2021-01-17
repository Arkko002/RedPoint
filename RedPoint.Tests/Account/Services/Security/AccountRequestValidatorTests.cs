using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using NLog.Fluent;
using RedPoint.Account.Exceptions;
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
        private readonly IdentityUser _identityUser = new() {Id = "1", UserName = "Username"};
        private readonly UserLoginDto _loginDto = new() {Username = "Username", Password = "Password"};
        private readonly UserRegisterDto _registerDto = new() {Username = "Username", Password = "Password"};
        
        private readonly Mock<MockUserManager<IdentityUser>> _userManager;
        private readonly Mock<MockSignInManager<IdentityUser>> _signInManager;
        private readonly Mock<UserValidator<IdentityUser>> _userValidator;

        private readonly AccountRequestValidator _requestValidator;

        private readonly Mock<IdentityResult> _identityResult;
        
        public AccountRequestValidatorTests()
        {
            var configurationProvider = new Mock<IAccountSecurityConfigurationProvider>();
            configurationProvider.Setup(x => x.GetBlacklistedPasswords())
                .Returns(new List<string> {InsecurePassword});

            var users = new List<IdentityUser>
            {
                _identityUser
            }.AsQueryable();

            _userManager = new Mock<MockUserManager<IdentityUser>>();
            _userManager.Setup(x => x.Users).Returns(users);
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(x => x.CheckPasswordAsync(_identityUser, It.IsAny<string>()))
                .ReturnsAsync(true);
            _userManager.Setup(x => x.IsLockedOutAsync(_identityUser))
                .ReturnsAsync(false);
                
            _signInManager = new Mock<MockSignInManager<IdentityUser>>(MockBehavior.Loose, _userManager);
            _signInManager.Setup(x => x.CanSignInAsync(_identityUser))
                .ReturnsAsync(true);
                
            _userValidator = new Mock<UserValidator<IdentityUser>>();

            _identityResult = new Mock<IdentityResult>();
            
            _requestValidator = new AccountRequestValidator(configurationProvider.Object, _signInManager.Object,
                _userValidator.Object, _userManager.Object);
        }

        [Fact]
        public async void LoginRequest_ValidLoginRequest_ShouldReturn()
        {
            await _requestValidator.IsLoginRequestValid(_loginDto);

            //Will assert only on valid login request
            Assert.True(true);
        }

        [Fact]
        public void LoginRequest_LockedOut_ShouldThrowLockedOut()
        {
            _userManager.Setup(x => x.IsLockedOutAsync(_identityUser))
                .ReturnsAsync(true);

            Assert.Throws<LockOutException>(
                () => _requestValidator.IsLoginRequestValid(_loginDto).ConfigureAwait(false));
        }

        [Fact]
        public void LoginRequest_CantSignIn_ShouldThrowAccountLogin()
        {
            _signInManager.Setup(x => x.CanSignInAsync(_identityUser))
                .ReturnsAsync(false);

            Assert.Throws<AccountLoginException>(() =>
                _requestValidator.IsLoginRequestValid(_loginDto).ConfigureAwait(false));
        }

        [Fact]
        public void LoginRequest_BadPassword_ShouldThrowAccountLogin()
        {
            _userManager.Setup(x => x.CheckPasswordAsync(_identityUser, It.IsAny<string>()))
                .ReturnsAsync(false);

            Assert.Throws<AccountLoginException>(() =>
                _requestValidator.IsLoginRequestValid(_loginDto).ConfigureAwait(false));
        }

        [Fact]
        public async void RegisterRequest_ValidRequest_ShouldReturn()
        {
            _identityResult.Setup(x => x.Succeeded).Returns(true);
            _userValidator.Setup(x => x.ValidateAsync(_userManager.Object, _identityUser))
                .ReturnsAsync(_identityResult.Object);

            await _requestValidator.IsRegisterRequestValid(_registerDto);

            //Will only assert on valid request
            Assert.True(true);
        }

        [Fact]
        public void RegisterRequest_WeakPassword_ShouldThrowAccountCreation()
        {
            var user = new UserRegisterDto
            {
                Username = "Username",
                Password = InsecurePassword
            };

            Assert.Throws<AccountCreationException>(() =>
                _requestValidator.IsRegisterRequestValid(user).ConfigureAwait(false));
        }

        [Fact]
        public void RegisterRequest_ValidationError_ShouldThrowAccountCreationWithListOfErrors()
        {
            var errors = new List<IdentityError>
            {
                new()
                {
                    Code = "TestCode",
                    Description = "TestDescription"
                }
            };
            _identityResult.Setup(x => x.Errors).Returns(errors);
            _identityResult.Setup(x => x.Succeeded).Returns(false);
            _userValidator.Setup(x => x.ValidateAsync(_userManager.Object, _identityUser))
                .ReturnsAsync(_identityResult.Object);

            void Act() => _requestValidator.IsRegisterRequestValid(_registerDto);
            var exception = Assert.Throws<AccountCreationException>(Act);                
            Assert.True(exception.Errors.All(x => x == "TestDescription"));
        }
    }
}