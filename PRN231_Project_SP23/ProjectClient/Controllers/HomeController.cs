using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using ProjectClient.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ProjectClient.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string UserApiUrl = "";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            UserApiUrl = "http://localhost:5000/api/User/Login";
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            HttpResponseMessage response = await client.GetAsync(UserApiUrl + "/" +email + "/" + password);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            string tokenString = JsonSerializer.Deserialize<string>(strData);
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(tokenString);
            foreach(var claim in jwtToken.Claims)
            {
                var claimValue = claim.Value;
                var claimType = claim.Type;
                var test = "";
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}