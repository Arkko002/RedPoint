using Microsoft.AspNetCore.Mvc;

namespace RedPoint.Controllers
{
    public class HomeController : Controller
    {
        //GET - /
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}