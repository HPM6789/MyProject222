using AutoMapper;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class MyMapperProfile : Profile
    {
        public MyMapperProfile()
        {
            CreateMap<Course, CourseDto>()
                ;
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(source => source.Role.RoleName));
            ;
            CreateMap<UserDto, User>()
                ;
            CreateMap<Role, RoleDto>();
            CreateMap<Material, MaterialDto>()
                ;
        }
    }
}
