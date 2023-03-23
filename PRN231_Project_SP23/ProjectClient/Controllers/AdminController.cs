using Microsoft.AspNetCore.Mvc;

namespace ProjectClient.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
