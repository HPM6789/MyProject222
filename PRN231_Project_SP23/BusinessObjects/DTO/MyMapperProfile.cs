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
            CreateMap<ViewModel.UploadAssignmentViewModel, AssigmentDto>()
                ;
            CreateMap<Assignment, AssigmentDto>().ForMember(dest => dest.TeacherName, opt => opt.MapFrom(source => source.Uploader.Fullname));
            CreateMap<AssigmentDto, Assignment>();
            CreateMap<Role, RoleDto>();
            CreateMap<Material, MaterialDto>()
                .ForMember(dest => dest.UploaderName, opt => opt.MapFrom(src => src.Uploader.Fullname))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
                ;
        }
    }
}
