using Microsoft.AspNetCore.Mvc;
using NLog;

namespace RedPoint.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly Logger _logger;

        public HomeController(Logger logger)
        {
            _logger = logger;
        }
    }
}
