using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository;
using BusinessObjects.Models;
using BusinessObjects.DTO;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly ICourseRepository _teacherRepository = new CourseRepository();
        private readonly IUserRepository _userRepository = new UserRepository();

        private readonly IMapper _mapper;

        public TeacherController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetAllCourses(int teacherId)
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
        public async Task<IActionResult> GetTeacherByEmail(string email)
        {
            User user = _userRepository.GetUserByEmail(email);
            UserDto userDto = _mapper.Map<User,UserDto>(user);
            return Ok(userDto);
        }
    }
}
