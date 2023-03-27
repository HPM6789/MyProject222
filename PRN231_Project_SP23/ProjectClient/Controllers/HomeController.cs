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
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
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

        public async Task<IActionResult> ErrorCode(System.Net.HttpStatusCode statusCodes)
        {
            string status = statusCodes.ToString();
            return View("ErrorCode", status);
        }
    }
}