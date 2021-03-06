using Moq;
using RedPoint.Account.Controllers;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Services;
using Xunit;

namespace RedPoint.Tests.Account.Controllers
{
    public class AccountControllerTests
    {
        private readonly AccountController _controller;

        private readonly Mock<IAccountService> _service;

        public AccountControllerTests()
        {
            _service = new Mock<IAccountService>();
            _controller = new AccountController(_service.Object);
        }

        [Fact]
        public void Delete_ShouldCallDeleteService()
        {
            _service.Setup(x => x.Delete(It.IsAny<UserLoginDto>()));

            var returnValue = _controller.Delete(new UserLoginDto {Username = "test", Password = "test"}).Result;

            _service.Verify(x => x.Delete(It.IsAny<UserLoginDto>()), Times.Once);
        }

        [Fact]
        public void Login_ShouldCallLoginService()
        {
            _service.Setup(x => x.Login(It.IsAny<UserLoginDto>()))
                .ReturnsAsync("CALLED");

            var returnValue = _controller.Login(new UserLoginDto {Username = "test", Password = "test"}).Result;

            _service.Verify(x => x.Login(It.IsAny<UserLoginDto>()), Times.Once);
        }

        [Fact]
        public void Register_ShouldCallRegisterService()
        {
            _service.Setup(x => x.Register(It.IsAny<UserRegisterDto>()))
                .ReturnsAsync("CALLED");

            var returnValue = _controller.Register(new UserRegisterDto {Username = "test", Password = "test"}).Result;

            _service.Verify(x => x.Register(It.IsAny<UserRegisterDto>()), Times.Once);
        }
    }
}