using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly IMapper _mapper;

        public TeacherController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
