using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Models.Errors;
using RedPoint.Account.Services;
using RedPoint.Account.Services.Security;
using Xunit;

namespace RedPoint.Tests.Account.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRequestValidator> _requestValidator;
        private readonly AccountService _service;
        private readonly Mock<ITokenGenerator> _tokenGenerator;

        public AccountServiceTests()
        {
            var users = new List<IdentityUser>
            {
                new()
                {
                    Id = "1",
                    UserName = "Username"
                }
            }.AsQueryable();

            var userManager = new Mock<UserManager<IdentityUser>>(new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.Users).Returns(users);
            userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);


            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var signInManager = new Mock<SignInManager<IdentityUser>>(userManager.Object, contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);

            _requestValidator = new Mock<IAccountRequestValidator>();
            _requestValidator.Setup(x => x.IsLoginRequestValid(It.IsAny<UserLoginDto>()))
                .Returns(Task.FromResult(new AccountError(AccountErrorType.NoError)));
            _requestValidator.Setup(x => x.IsRegisterRequestValid(It.IsAny<UserRegisterDto>()))
                .Returns(Task.FromResult(new AccountError(AccountErrorType.NoError)));

            _tokenGenerator = new Mock<ITokenGenerator>();
            _tokenGenerator.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<IdentityUser>()))
                .Returns("Token");
            _tokenGenerator.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<IdentityUser>()))
                .Returns("Token");

            _service = new AccountService(userManager.Object,
                _requestValidator.Object,
                signInManager.Object,
                _tokenGenerator.Object);
        }

        [Fact]
        public void Login_LoginRequest_ShouldVerifyRequestAndGenerateToken()
        {
            var dto = new UserLoginDto
            {
                Password = "Password",
                Username = "Username"
            };

            _service.Login(dto);

            _requestValidator.Verify(x => x.IsLoginRequestValid(dto), Times.AtLeastOnce);
            _tokenGenerator.Verify(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<IdentityUser>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Register_RegisterRequest_ShouldVerifyRequestAndGenerateToken()
        {
            var dto = new UserRegisterDto
            {
                Password = "Password",
                Username = "Username"
            };

            _service.Register(dto);

            _requestValidator.Verify(x => x.IsRegisterRequestValid(dto), Times.AtLeastOnce);
            _tokenGenerator.Verify(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<IdentityUser>()),
                Times.AtLeastOnce);
        }
    }
}