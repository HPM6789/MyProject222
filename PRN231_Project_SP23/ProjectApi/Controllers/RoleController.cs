using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository;
using Microsoft.AspNetCore.Authorization;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IUserRepository _userRepository = new UserRepository();
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RoleController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpGet("GetAllRoles")]
        public ActionResult<IEnumerable<RoleDto>> GetAllRoles() => _userRepository.GetAllRoles().Select(_mapper.Map<Role, RoleDto>).ToList();

    }
}
