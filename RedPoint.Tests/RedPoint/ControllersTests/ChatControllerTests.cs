using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using RedPoint.Areas.Chat.Controllers;

namespace RedPoint.Tests.RedPoint.ControllersTests
{
    [TestFixture]
    class ChatControllerTests
    {
        private ChatController _chatController;

        [SetUp]
        public void SetUp()
        {
            _chatController = new ChatController();
        }

        [Test]
        public void Index_ReturnsViewResult()
        {
            //act
            var result = _chatController.Index();

            //assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void AddServer_ReturnsPartialViewResult()
        {
            //act
            var result = _chatController.AddServer();

            //assert
            Assert.IsInstanceOf<PartialViewResult>(result);
        }

        [Test]
        public void AddChannel_ReturnsPartialViewResult()
        {
            //act
            var result = _chatController.AddServer();

            //assert
            Assert.IsInstanceOf<PartialViewResult>(result);
        }
    }
}
