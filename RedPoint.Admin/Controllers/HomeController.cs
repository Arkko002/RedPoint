using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using RedPoint.Admin.Models;

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
