using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedPoint.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
    }
}