using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }

        public List<Assignment> Assignments { get; set; }
        public List<Material> Materials { get; set; }

        public List<User> Users { get; set; }
    }
}
