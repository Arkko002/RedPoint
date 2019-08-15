using Microsoft.AspNetCore.Mvc;

namespace RedPoint.Areas.MainPage.Controllers
{
    public class HomeController : Controller
    {
        //GET - /
        [HttpGet]
        [Area("mainpage")]
        public ActionResult Index()
        {
            return View();
        }
    }
}