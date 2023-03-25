﻿using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CourseDao
    {
        public static IEnumerable<Course> GetAllCourseByTeacherId(int teacherId)
        {
            List<Course> list = new List<Course>();
            try
            {
                using (var context = new PRN231_ProjectContext())
                {
                    var user = context.Users.Where(u => u.UserId == teacherId).FirstOrDefault();
                    var courses = context.Courses.Include(c => c.Assignments).Include(u => u.Users).Where(u => u.Users.Contains(user)).ToList();
                    foreach(var c in courses)
                    {
                        list.Add(c);
                    }
                }
            }catch (Exception ex) {
                throw new Exception();
            }
            return list;
        }

        public static IEnumerable<Course> GetAllCourseByStudentId(int studentId)
        {
            List<Course> list = new List<Course>();
            try
            {
                using (var context = new PRN231_ProjectContext())
                {
                    var user = context.Users.Where(u => u.UserId == studentId).FirstOrDefault();
                    var courses = context.Courses.Include(c => c.Assignments).Include(u => u.Users).Where(u => u.Users.Contains(user)).ToList();
                    foreach (var c in courses)
                    {
                        list.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return list;
        }
    }
}
