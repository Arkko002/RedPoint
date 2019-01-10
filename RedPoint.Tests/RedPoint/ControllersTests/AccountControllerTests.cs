using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using RedPoint.Controllers;

namespace RedPoint.Tests.RedPoint.ControllersTests
{
    [TestFixture]
    class AccountControllerTests
    {
        private AccountController _accountController;

        [SetUp]
        public void SetUp()
        {
            _accountController = new AccountController();
        }

        [Test]
        public void Index_ReturnsActionResult()
        {
            //act
            var result = _accountController.Index();

            //assert
            Assert.IsInstanceOf<ActionResult>(result);
        }
    }
}
