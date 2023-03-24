using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository;
using BusinessObjects.Models;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository = new TeacherRepository();

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

            return Ok(courses);
        }
    }
}
