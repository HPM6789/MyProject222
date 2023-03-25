using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository;
using BusinessObjects.Models;
using BusinessObjects.DTO;
using BusinessObjects.ViewModel;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly ICourseRepository _teacherRepository = new CourseRepository();
        private readonly IUserRepository _userRepository = new UserRepository();
        private readonly IMaterialRepository _materialRepository= new MaterialRepository();
        private string BaseMaterailUrl = "";
        private readonly IMapper _mapper;

        public TeacherController(IMapper mapper)
        {
            _mapper = mapper;
            BaseMaterailUrl = "../ProjectApi/wwwroot/AllFiles/Materials";
        }
        [HttpGet("{teacherId}")]
        public IActionResult GetAllCourses(int teacherId)
        {
            List<Course> courses = (List<Course>)_teacherRepository.GetAllCourseByTeacherId(teacherId);
            if(courses == null || courses.Count == 0)
            {
                return NotFound();
            }
            List<CourseDto> courseDtos= new List<CourseDto>();
            foreach(var c in courses)
            {
                courseDtos.Add(_mapper.Map<Course, CourseDto>(c));
                
            }
            return Ok(courseDtos);
        }
        [HttpGet("{email}")]
        public IActionResult GetTeacherByEmail(string email)
        {
            User user = _userRepository.GetUserByEmail(email);
            UserDto userDto = _mapper.Map<User,UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("{courseId}")]
        public IActionResult GetAllMaterialsByCourse(int courseId)
        {
            List<Material> materials = (List<Material>)_materialRepository.GetMaterialsByCourseId(courseId);
            List<MaterialDto> materialDtos = new List<MaterialDto>();
            foreach(var material in materials)
            {
                materialDtos.Add(_mapper.Map<Material,MaterialDto>(material));
            }
            return Ok(materialDtos);
        }

        [HttpPost]
        public IActionResult UploadMaterial ([FromForm] IFormFile file, [FromForm] int courseId, [FromForm] int uploaderId)
        {
            UploadMaterialViewModel uploadMaterialViewModel = new UploadMaterialViewModel();
            uploadMaterialViewModel.Material = file;
            uploadMaterialViewModel.CourseId = courseId;
            uploadMaterialViewModel.UploaderId = uploaderId;
            uploadMaterialViewModel.MaterialPath = BaseMaterailUrl;
            uploadMaterialViewModel.MaterialName = file.FileName;
            _materialRepository.SaveMaterial(uploadMaterialViewModel.Material, uploadMaterialViewModel.MaterialPath,
                uploadMaterialViewModel.CourseId, uploadMaterialViewModel.UploaderId, uploadMaterialViewModel.MaterialName);
            return Ok();
        }

        [HttpDelete("{materialId}")]
        public IActionResult DeleteMaterial(int materialId)
        {
            var material = _materialRepository.GetMaterialById(materialId);
            if (System.IO.File.Exists(material.Path + "/" + material.MaterialName))
            {
                System.IO.File.Delete(material.Path + "/" + material.MaterialName);
            }
            _materialRepository.DeleteMaterial(materialId);
            return Ok();
        }
    }
}
