using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
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
            ViewData["teacherId"] = teacherId;
            return View(courses);
        }

        public async Task<IActionResult> ListMaterialOfCourse(int id, string msg)
        {
            HttpResponseMessage response = await client.GetAsync(TeacherApiUrl + "/GetAllMaterialsByCourse/" + id.ToString());
            string materialJson = await response.Content.ReadAsStringAsync();
            List<MaterialDto> materialDtos = JsonSerializer.Deserialize<List<MaterialDto>>(materialJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            ViewData["courseId"] = id;
            ViewData["msg"] = msg;
            return View(materialDtos);
        }

        [HttpPost]
        public async Task<IActionResult> UploadMaterial(List<IFormFile> materials, int courseId)
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
            if(materials == null || materials.Count <= 0)
            {
                return RedirectToAction("ListMaterialOfCourse", "Teacher", new { id = courseId });
            }


            var content = new MultipartFormDataContent();

            foreach(var material in materials)
            {
                using var stream = material.OpenReadStream();
                var streamContent = new StreamContent(stream);
                var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
                content.Add(fileContent, "files", material.FileName);
            }
            content.Add(new StringContent(courseId.ToString()), "courseId");
            content.Add(new StringContent(uploaderId.ToString()), "uploaderId");

            var postTask = await client.PostAsync(TeacherApiUrl + "/UploadMaterial", content);
            string msg = "";
            if (postTask.IsSuccessStatusCode)
            {
                msg = "success";
            }
            else
            {
                msg = "failed";
            }
            return RedirectToAction("ListMaterialOfCourse", "Teacher", new {id = courseId, msg = msg});
        }

        public async Task<IActionResult> DeleteMaterial(int materialId, int courseId)
        {
            var deleteTask = await client.DeleteAsync(TeacherApiUrl + "/DeleteMaterial/" + materialId.ToString());

            return RedirectToAction("ListMaterialOfCourse", "Teacher", new { id = courseId });
        }
        [HttpGet]
        public async Task<IActionResult> UploadAssignment()
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
            // co uploaderId
            //tao item
            ViewData["CourseId"] = await listCourseByUploaderId(uploaderId);
            UploadAssignmentViewModel model = new UploadAssignmentViewModel();
            model.UploaderId = uploaderId;
            return View(model);
        }
        private async Task<SelectList> listCourseByUploaderId(int uploaderId)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(TeacherApiUrl + "/GetAllCourses/" + uploaderId.ToString());
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var courses = JsonSerializer.Deserialize<List<CourseDto>>(strData, options);
            return new SelectList(courses, "CourseId", "CourseName");
        }
        public async Task<IActionResult> ListAssignmentByCourse(int tid, int cid)
        {
            HttpResponseMessage response = await client.GetAsync(TeacherApiUrl + $"/ListAssignmentByCourse/{tid}/{cid}");
            var stream = await response.Content.ReadAsStreamAsync();
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<AssigmentDto> assigmentDtos = JsonSerializer.Deserialize<List<AssigmentDto>>(strData, options);
            return View(assigmentDtos);
        }

        public async Task<IActionResult> ListSubmitAssignmentByAssId(int assId)
        {
            HttpResponseMessage response = await client.GetAsync(TeacherApiUrl + $"/ListSubmitAssignmentByCourse/{assId}");
            var stream = await response.Content.ReadAsStreamAsync();
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<SubmitAssignmentDto> subAssigmentDtos = JsonSerializer.Deserialize<List<SubmitAssignmentDto>>(strData, options);
            return View(subAssigmentDtos);
        }
    }
}
