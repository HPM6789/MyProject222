using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Repository;
using Repository.Interfaces;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Student")]

    public class StudentController : Controller
    {
        private readonly ICourseRepository _courseRepository = new CourseRepository();
        private readonly IMaterialRepository _materialRepository = new MaterialRepository();
        private readonly IUserRepository _userRepository = new UserRepository();

        private readonly IMapper _mapper;

        public StudentController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("{materialId}")]
        public IActionResult DowloadMaterial(int materialId)
        {
            var material = _materialRepository.GetMaterialById(materialId);
            string materialPath = material.Path + "/" + material.MaterialName;
            var fileExtension = Path.GetExtension(material.MaterialName);
            var contentType = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + fileExtension, "Content Type", null) as string;
            Byte[] b = System.IO.File.ReadAllBytes(materialPath);
            return File(b, contentType, material.MaterialName);
        }
        [HttpGet("{studentId}")]
        public IActionResult GetAllCourses(int studentId)
        {
            List<Course> courses = (List<Course>)_courseRepository.GetAllCourseByStudentId(studentId);
            if (courses == null || courses.Count == 0)
            {
                return NotFound();
            }
            List<CourseDto> courseDtos = new List<CourseDto>();
            foreach (var c in courses)
            {
                courseDtos.Add(_mapper.Map<Course, CourseDto>(c));

            }
            return Ok(courseDtos);
        }
        [HttpGet("{email}")]
        public IActionResult GetStudentByEmail(string email)
        {
            User user = _userRepository.GetUserByEmail(email);
            UserDto userDto = _mapper.Map<User, UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("{courseId}")]
        public IActionResult GetAllMaterialsByCourse(int courseId)
        {
            List<Material> materials = (List<Material>)_materialRepository.GetMaterialsByCourseId(courseId);
            List<MaterialDto> materialDtos = new List<MaterialDto>();
            foreach (var material in materials)
            {
                materialDtos.Add(_mapper.Map<Material, MaterialDto>(material));
            }
            return Ok(materialDtos);
        }
    }
}
