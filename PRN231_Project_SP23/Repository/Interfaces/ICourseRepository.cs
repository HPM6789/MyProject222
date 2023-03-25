using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> GetAllCourseByTeacherId(int teacherId);
        public IEnumerable<Course> GetAllCourseByStudentId(int studentId);
    }
}
