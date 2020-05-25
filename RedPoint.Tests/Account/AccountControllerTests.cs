using Moq;
using RedPoint.Areas.Account.Controllers;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services;
using Xunit;

namespace RedPoint.Tests.Account
{
    public class AccountControllerTests
    {
        public AccountControllerTests()
        {
            _service = new Mock<IAccountService>();
            _controller = new AccountController(_service.Object);
        }

        private readonly Mock<IAccountService> _service;
        private readonly AccountController _controller;

        [Fact]
        public void Delete_ShouldCallDeleteService()
        {
            _service.Setup(x => x.Delete(It.IsAny<UserLoginDto>()))
                .ReturnsAsync(true);

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