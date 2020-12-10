using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Account.Models.Account;
using RedPoint.Account.Services;

namespace RedPoint.Account.Controllers
{
    /// <summary>
    /// Controller for account-related functionality.
    /// </summary>
    //TODO Change the return values of controller, possibly to IActionResult
    [Route("{controller}/{action}")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var token = await _accountService.Login(model);
            return Ok(new {token});
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
        {
            var token = await _accountService.Register(model);
            return Ok(new {token});
        }

        [HttpPost]
        [Authorize]
        //TODO
        public async Task<object> Delete([FromBody] UserLoginDto model)
        {
            return await _accountService.Delete(model);
        }
    }
}