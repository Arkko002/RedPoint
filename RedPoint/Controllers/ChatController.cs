using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedPoint.Controllers
{
    #if DEBUG
    #else
        [Authorize] 
    #endif
    public class ChatController : Controller
    {       
        // GET: Chat
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult AddServer()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult AddChannel()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult BrowseServers()
        {
            return PartialView();
        }
    }
}