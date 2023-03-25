using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccess;
using Repository.Interfaces;

namespace Repository
{
    public class CourseRepository : ICourseRepository
    {
        public IEnumerable<Course> GetAllCourseByStudentId(int studentId) => CourseDao.GetAllCourseByStudentId(studentId);

        public IEnumerable<Course> GetAllCourseByTeacherId(int teacherId) => CourseDao.GetAllCourseByTeacherId(teacherId);
    }
}
