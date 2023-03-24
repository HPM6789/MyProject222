using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TeacherDao
    {
        public static IEnumerable<Course> GetAllCourseByTeacherId(int teacherId)
        {
            List<Course> list = new List<Course>();
            try
            {
                using (var context = new PRN231_ProjectContext())
                {
                    var courses = context.Users.Include(u => u.Courses).Where(u => u.UserId == teacherId).Select(u => u.Courses).ToList();
                    foreach(var c in courses)
                    {
                        list.Add((Course)c);
                    }
                }
            }catch (Exception ex) {
                throw new Exception();
            }
            return list;
        }
    }
}
