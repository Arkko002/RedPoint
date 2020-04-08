using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedPoint.Areas.Account.Services;
using RedPoint.Exceptions;
using RedPoint.Services;

namespace RedPoint.Areas.Identity.Controllers
{
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
            try
            {
                _accountService.ValidateLoginRequest(); //TODO
                return await _accountService.Login(model);
            }
            catch (Exception e)
            {
                //TODO Log errors
                return null;
            }
        }

        [HttpPost]
        public async Task<object> Register([FromBody] UserRegisterDto model)
        {
            try
            {
                _accountService.ValidateRegisterRequest(model);
                return await _accountService.Register(model);
            }
            catch (Exception e)
            {
                //TODO Log erros
                return null;
            }
        }
    }
}