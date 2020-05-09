using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Account.Services;

namespace RedPoint.Tests.Account
{
    public class AccountServiceTests
    {
        private AccountService _service;

        public AccountServiceTests()
        {
            //TODO
            MockConfiguration configuration = new MockConfiguration("empty.txt");

            //_service = new AccountService();
        }
    }
}