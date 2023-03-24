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
    public class TeacherRepository : ITeacherRepository
    {
        public IEnumerable<Course> GetAllCourseByTeacherId(int teacherId) => TeacherDao.GetAllCourseByTeacherId(teacherId);
    }
}
