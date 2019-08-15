using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RedPoint.Areas.Admin.Controllers;

namespace RedPoint.Tests.RedPoint.ControllersTests
{
    [TestFixture]
    class AdminControllerTests
    {
        private AdminController _adminController;

        [SetUp]
        public void SetUp()
        {
            _adminController = new AdminController();
        }
    }
}
