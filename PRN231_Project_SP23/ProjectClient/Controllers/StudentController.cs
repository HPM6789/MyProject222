using Microsoft.AspNetCore.Mvc;

namespace ProjectClient.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient client = null;
        private string StudentApiUrl = "";

        public StudentController()
        {
            client = new HttpClient();
            StudentApiUrl = "";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
