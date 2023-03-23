using Microsoft.AspNetCore.Mvc;

namespace ProjectClient.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
