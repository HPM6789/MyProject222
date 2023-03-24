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
                .ForMember(dest => dest.Materials, act => act.MapFrom(src =>src.Materials.ToList()))
                .ForMember(dest => dest.Assignments, act => act.MapFrom(src =>src.Assignments.ToList()))
                .ForMember(dest => dest.Users, act => act.MapFrom(src =>src.Users.ToList()))
                ;
        }
    }
}
