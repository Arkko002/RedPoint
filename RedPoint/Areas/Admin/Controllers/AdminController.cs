﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedPoint.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
    }
}