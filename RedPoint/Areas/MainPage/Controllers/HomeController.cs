using Microsoft.AspNetCore.Mvc;

namespace RedPoint.Areas.MainPage.Controllers
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