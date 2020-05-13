using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services;
using RedPoint.Exceptions;

namespace RedPoint.Areas.Account.Controllers
{
    //TODO Change the return values of controller, possibly to IActionResult
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] UserLoginDto model)
        {
            return await _accountService.Login(model);
        }

        [HttpPost]
        public async Task<object> Register([FromBody] UserRegisterDto model)
        {
            return await _accountService.Register(model);
        }

        [HttpPost]
        public async Task<object> Delete([FromBody] UserLoginDto model)
        {
            return await _accountService.Delete(model);
        }
    }
}