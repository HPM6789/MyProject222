using Microsoft.AspNetCore.Mvc;

namespace ProjectClient.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
