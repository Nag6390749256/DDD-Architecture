using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
