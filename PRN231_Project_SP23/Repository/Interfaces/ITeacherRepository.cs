using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITeacherRepository
    {
        public IEnumerable<Course> GetAllCourseByTeacherId(int teacherId);
    }
}
