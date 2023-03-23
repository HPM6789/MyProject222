using Microsoft.AspNetCore.Mvc;

namespace ProjectClient.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(string username, string myPassword)
        {
            //TODO: connect with database
            if (username != null && myPassword != null && username.Equals("admin") && myPassword.Equals("123"))
            {
                HttpContext.Session.SetString("username", username);
                return View("Welcome");
            }
            else
            {
                ViewBag.error = "Invalid";
                return View("Index");
            }
        }
    }
}
