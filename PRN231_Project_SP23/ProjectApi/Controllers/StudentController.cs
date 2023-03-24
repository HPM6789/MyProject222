using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Student")]

    public class StudentController : Controller
    {
        private readonly IMapper _mapper;

        public StudentController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult TestApi()
        {
            return Ok();
        }
    }
}
