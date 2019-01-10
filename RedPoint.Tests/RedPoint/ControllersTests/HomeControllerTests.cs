using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using RedPoint.Controllers;

namespace RedPoint.Tests.RedPoint.ControllersTests
{
    [TestFixture]
    class HomeControllerTests
    {
        private HomeController _homeController;

        [SetUp]
        public void SetUp()
        {
            _homeController = new HomeController();
        }

        [Test]
        public void Index_ReturnsActionResult()
        {
            //act
            var result = _homeController.Index();

            //assert
            Assert.IsInstanceOf<ActionResult>(result);
        }
    }
}
