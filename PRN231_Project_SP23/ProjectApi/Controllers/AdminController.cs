﻿using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Interfaces;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICourseRepository _courseRepository = new CourseRepository();
        private readonly IMapper _mapper;

        public AdminController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllCourse()
        {
            List<Course> courses = _courseRepository.GetAllCourse();
            List<CourseDto> courseDtos = new List<CourseDto>();
            foreach(var course in courses)
            {
                courseDtos.Add(_mapper.Map<Course, CourseDto>(course));
            }
            return Ok(courseDtos);
        }

        [HttpGet("{courseId}")]
        public IActionResult GetCourseById(int courseId)
        {
            Course course = _courseRepository.GetCourseById(courseId);
            CourseDto courseDto = _mapper.Map<Course, CourseDto>(course);
            return Ok(courseDto);
        }
        [HttpPost]
        public IActionResult InsertCourse(Course course) {
            try
            {
                _courseRepository.InsertCourse(course);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCourse(Course course)
        {
            try
            {
                _courseRepository.UpdateCourse(course);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{courseId}")]
        public IActionResult DeleteCourse(int courseId)
        {
            var c = _courseRepository.GetCourseById(courseId);
            if(c == null)
            {
                return NotFound();
            }
            var materialCourse = c.Materials.Where(a => a.CourseId == courseId).ToList();
            List<string> mPaths = c.Materials.Select(m => m.Path + "/" + m.MaterialName).ToList();
            foreach (var m in mPaths)
            {
                if (System.IO.File.Exists(m))
                {
                    System.IO.File.Delete(m);
                }
            }
            _courseRepository.DeleteCourse(c);
            
            return Ok();
        }
    }
}
