using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RedPoint.Areas.Account.Controllers;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services;
using RedPoint.Areas.Account.Services.Security;
using Xunit;

namespace RedPoint.Tests.Account
{
    public class AccountControllerTests
    {
        private Mock<IAccountService> _service;
        private AccountController _controller;

        public AccountControllerTests()
        {
            _service = new Mock<IAccountService>();
            _controller = new AccountController(_service.Object);
        }

        [Fact]
        public void Login_ShouldCallLoginService()
        {
            _service.Setup(x => x.Login(It.IsAny<UserLoginDto>()))
                .ReturnsAsync("CALLED");

            var returnValue = _controller.Login(new UserLoginDto {Username = "test", Password = "test"}).Result;
            
            Assert.True((string)returnValue == "CALLED");
        }

        [Fact]
        public void Register_ShouldCallRegisterService()
        {
            _service.Setup(x => x.Register(It.IsAny<UserRegisterDto>()))
                .ReturnsAsync("CALLED");

            var returnValue = _controller.Register(new UserRegisterDto {Username = "test", Password = "test"}).Result;
            
            Assert.True((string)returnValue == "CALLED");
        }

        [Fact]
        public void Delete_ShouldCallDeleteService()
        {
            _service.Setup(x => x.Delete(It.IsAny<UserLoginDto>()))
                .ReturnsAsync(true);

            var returnValue = _controller.Delete(new UserLoginDto {Username = "test", Password = "test"}).Result;
            
            Assert.True((bool)returnValue);
        }
    }
}