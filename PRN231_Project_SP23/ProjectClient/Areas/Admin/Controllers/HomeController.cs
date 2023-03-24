using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //tao select item
            ViewData["RoleId"] = await listRole();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            //tao item
            ViewData["RoleId"] = await listRole();
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid ModelState";
                return View(userDto);
            }
            HttpResponseMessage getData = await client.PostAsJsonAsync<UserDto>("api/User/PostUser", userDto);
            //getData.EnsureSuccessStatusCode();
            if (getData.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ErrorMessage"] = "Create fail";
                return View(userDto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int uid)
        {
            ViewData["RoleId"] = await listRole();
            UserDto userDto = await getUserDTOById(uid);
            return View(userDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDto userDto)
        {
            //tao item
            ViewData["RoleId"] = await listRole();
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "ModelState is invalid";
                return View(userDto);
            }
            HttpResponseMessage getData = await client.PutAsJsonAsync<UserDto>($"api/User/UpdateProduct/uid?uid={userDto.UserId}", userDto);
            //getData.EnsureSuccessStatusCode();
            if (getData.IsSuccessStatusCode)
            {
                //return Redirect($"Details?pid={userDto.ProductId}");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ErrorMessage"] = "Edit fail";
                return View(userDto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int uid)
        {
            string message = "";
            HttpResponseMessage response = await client.DeleteAsync(
            $"api/User/DeleteUser/uid?uid={uid}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                message = "Delete fail";
            }
            return Content(message);
        }
        private async Task<SelectList> listRole()
        {
            //lay list category
            UserApiUrl = "http://localhost:5000/api/Role/GetAllRoles";
            HttpResponseMessage response = await client.GetAsync(UserApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var roles = JsonSerializer.Deserialize<List<RoleDto>>(strData, options);
            return new SelectList(roles, "RoleId", "RoleName");
        }
        private async Task<UserDto> getUserDTOById(int uid)
        {
            //
            UserApiUrl = $"http://localhost:5000/api/User/GetUserById/uid?uid={uid}";
            HttpResponseMessage response = await client.GetAsync(UserApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            //
            //Deserialize dữ liệu
            try
            {
                UserDto userDto = JsonSerializer.Deserialize<UserDto>(strData, options);
                return userDto;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
