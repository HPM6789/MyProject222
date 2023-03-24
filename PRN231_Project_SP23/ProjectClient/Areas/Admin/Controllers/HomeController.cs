using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectClient.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string UserApiUrl = "";
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> Index()
        {
            UserApiUrl = "http://localhost:5000/api/User/GetAllUsers";
            HttpResponseMessage response = await client.GetAsync(UserApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<UserDto>? listUserDtos = JsonSerializer.Deserialize<List<UserDto>>(strData, options);
            return View(listUserDtos);
        }
    }
}
