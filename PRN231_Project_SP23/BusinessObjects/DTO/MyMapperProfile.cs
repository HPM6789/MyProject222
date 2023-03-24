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
                ;
            CreateMap<UserDto, User>()
                ;
            CreateMap<Material, MaterialDto>()
                ;
        }
    }
}
