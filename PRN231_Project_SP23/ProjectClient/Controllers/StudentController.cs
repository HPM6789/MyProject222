using BusinessObjects.DTO;
using BusinessObjects.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ProjectClient.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient client = null;
        private string StudentApiUrl = "";

        public StudentController()
        {
            client = new HttpClient();
            StudentApiUrl = "http://localhost:5000/api/Student";
        }

        public async Task<IActionResult> ListCourse()
        {
            int studentId = 0;
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

            HttpResponseMessage responseMessage2 = await client.GetAsync(StudentApiUrl + "/GetStudentByEmail/" + email);
            string strData2 = await responseMessage2.Content.ReadAsStringAsync();
            UserDto user = JsonSerializer.Deserialize<UserDto>(strData2, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            studentId = user.UserId;
            HttpResponseMessage responseMessage = await client.GetAsync(StudentApiUrl + "/GetAllCourses/" + studentId.ToString());
            string courseJson = await responseMessage.Content.ReadAsStringAsync();
            List<CourseDto> courses = JsonSerializer.Deserialize<List<CourseDto>>(courseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return View(courses);
        }

        public async Task<IActionResult> ListMaterialOfCourse(int id)
        {
            HttpResponseMessage response = await client.GetAsync(StudentApiUrl + "/GetAllMaterialsByCourse/" + id.ToString());
            string materialJson = await response.Content.ReadAsStringAsync();
            List<MaterialDto> materialDtos = JsonSerializer.Deserialize<List<MaterialDto>>(materialJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            ViewData["courseId"] = id;
            return View(materialDtos);
        }

        public async Task<IActionResult> DownloadMaterial(int id)
        {
            HttpResponseMessage response = await client.GetAsync(StudentApiUrl + "/DowloadMaterial/" + id.ToString());
            var stream = await response.Content.ReadAsStreamAsync();
            var contentType = response.Content.Headers.ContentType.ToString();
            var fileName = response.Content.Headers.ContentDisposition.FileName.Trim('\"');
            return File(stream, contentType, fileName);
        }
        public async Task<IActionResult> ListAssignmentsByCourse(int id)
        {
            HttpResponseMessage response = await client.GetAsync(StudentApiUrl + "/GetAssignmentsByCourse/" + id.ToString());
            var stream = await response.Content.ReadAsStreamAsync();
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<AssigmentDto> assigmentDtos = JsonSerializer.Deserialize<List<AssigmentDto>>(strData, options);
            return View(assigmentDtos);
        }
        public IActionResult SubmitAssignment(int assid)
        {
            int uploaderId = 0;
            var strData = HttpContext.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(strData))
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtHandler.ReadJwtToken(strData);

                foreach (var claim in jwtToken.Claims)
                {
                    var type = claim.Type;
                    if (claim.Type.Equals("nameid"))
                    {
                        uploaderId = int.Parse(claim.Value);
                    }
                }
            }
            SubmitAssignmentViewModel model = new SubmitAssignmentViewModel();
            model.AssignmentId = assid;
            model.UploaderId = uploaderId;
            return View(model);
        }

    }
}
