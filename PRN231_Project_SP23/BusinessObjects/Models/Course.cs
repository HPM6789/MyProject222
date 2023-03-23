using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Course
    {
        public Course()
        {
            Assignments = new HashSet<Assignment>();
            Materials = new HashSet<Material>();
        }

        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
