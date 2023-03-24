using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ProjectClient.Controllers
{
    public class TeacherController : Controller
    {
        private readonly HttpClient client = null;
        private string TeacherApiUrl = "";

        public TeacherController()
        {
            client = new HttpClient();
            TeacherApiUrl = "http://localhost:5000/api/Teacher";
        }

        public async Task<IActionResult> ListIndex()
        {
            int teacherId = 0;
            var strData = Request.Cookies["jwtToken"];
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(strData);
            string email = "";
            foreach (var claim in jwtToken.Claims)
            {
                var type = claim.Type;
                if (claim.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
                {
                    email = claim.Value;
                }
            }
            HttpResponseMessage responseMessage2 = await client.GetAsync(TeacherApiUrl + "/GetTeacherByEmail/" + email);
            string strData2 = await responseMessage2.Content.ReadAsStringAsync();
            UserDto user = JsonSerializer.Deserialize<UserDto>(strData2, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            teacherId = user.UserId;
            HttpResponseMessage responseMessage = await client.GetAsync(TeacherApiUrl + "/GetAllCourses/" + teacherId.ToString());
            string courseJson = await responseMessage.Content.ReadAsStringAsync();
            List<CourseDto> courses = JsonSerializer.Deserialize<List<CourseDto>>(courseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return View(courses);
        }
    }
}
