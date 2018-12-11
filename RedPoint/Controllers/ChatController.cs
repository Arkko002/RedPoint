using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}