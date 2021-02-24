using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using RedPoint.Account.Exceptions;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Services.Security;
using Xunit;

namespace RedPoint.Tests.Account.Services.Security
{
    public class AccountRequestValidatorTests
    {
        private const string InsecurePassword = "InsecurePassword";
        private readonly IdentityUser _identityUser = new() {Id = "1", UserName = "Username"};
        private readonly UserLoginDto _loginDto = new() {Username = "Username", Password = "Password"};
        private readonly UserRegisterDto _registerDto = new() {Username = "Username", Password = "Password"};

        private readonly AccountRequestValidator _requestValidator;
        private readonly Mock<SignInManager<IdentityUser>> _signInManager;

        private readonly Mock<UserManager<IdentityUser>> _userManager;
        private readonly Mock<UserValidator<IdentityUser>> _userValidator;

        public AccountRequestValidatorTests()
        {
            var configurationProvider = new Mock<IAccountSecurityConfigurationProvider>();
            configurationProvider.Setup(x => x.GetBlacklistedPasswords())
                .Returns(new List<string> {InsecurePassword});

            var users = new List<IdentityUser>
            {
                _identityUser
            }.AsQueryable();

            _userManager = new Mock<UserManager<IdentityUser>>(new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null);
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(_identityUser);
            _userManager.Setup(x => x.Users).Returns(users);
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(x => x.CheckPasswordAsync(_identityUser, It.IsAny<string>()))
                .ReturnsAsync(true);
            _userManager.Setup(x => x.IsLockedOutAsync(_identityUser))
                .ReturnsAsync(false);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            _signInManager = new Mock<SignInManager<IdentityUser>>(_userManager.Object, contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);
            _signInManager.Setup(x => x.CanSignInAsync(_identityUser))
                .ReturnsAsync(true);


            _requestValidator = new AccountRequestValidator(configurationProvider.Object, _signInManager.Object,
                 _userManager.Object);
        }

        [Fact]
        public async void LoginRequest_ValidLoginRequest_ShouldReturn()
        {
            await _requestValidator.IsLoginRequestValid(_loginDto);

            //Will assert only on valid login request
            Assert.True(true);
        }

        [Fact]
        public async void LoginRequest_LockedOut_ShouldThrowLockedOut()
        {
            _userManager.Setup(x => x.IsLockedOutAsync(_identityUser))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<LockOutException>(() => _requestValidator.IsLoginRequestValid(_loginDto));
        }

        [Fact]
        public async void LoginRequest_CantSignIn_ShouldThrowAccountLogin()
        {
            _signInManager.Setup(x => x.CanSignInAsync(_identityUser))
                .ReturnsAsync(false);

            await Assert.ThrowsAsync<AccountLoginException>(() => _requestValidator.IsLoginRequestValid(_loginDto));
        }

        [Fact]
        public async void LoginRequest_BadPassword_ShouldThrowAccountLogin()
        {
            _userManager.Setup(x => x.CheckPasswordAsync(_identityUser, It.IsAny<string>()))
                .ReturnsAsync(false);

            await Assert.ThrowsAsync<AccountLoginException>(() => _requestValidator.IsLoginRequestValid(_loginDto));
        }

        [Fact]
        public async void RegisterRequest_ValidRequest_ShouldReturn()
        {
            _userValidator.Setup(x => x.ValidateAsync(It.IsAny<UserManager<IdentityUser>>(), It.IsAny<IdentityUser>()))
                .ReturnsAsync(IdentityResult.Success);

            await _requestValidator.IsRegisterRequestValid(_registerDto);

            //Will only assert on valid request
            Assert.True(true);
        }

        [Fact]
        public async void RegisterRequest_WeakPassword_ShouldThrowAccountCreation()
        {
            var user = new UserRegisterDto
            {
                Username = "Username",
                Password = InsecurePassword
            };

            await Assert.ThrowsAsync<AccountCreationException>(() => _requestValidator.IsRegisterRequestValid(user));
        }

        [Fact]
        public async void RegisterRequest_ValidationError_ShouldThrowAccountCreationWithListOfErrors()
        {
            var errors = new List<IdentityError>
            {
                new()
                {
                    Code = "TestCode",
                    Description = "TestDescription"
                }
            };
            _userValidator.Setup(x => x.ValidateAsync(It.IsAny<UserManager<IdentityUser>>(), It.IsAny<IdentityUser>()))
                .ReturnsAsync(IdentityResult.Failed(errors.ToArray()));

            Task Act()
            {
                return _requestValidator.IsRegisterRequestValid(_registerDto);
            }

            var exception = await Assert.ThrowsAsync<AccountCreationException>(Act);
            Assert.True(exception.Errors.All(x => x == "TestDescription"));
        }
    }
}