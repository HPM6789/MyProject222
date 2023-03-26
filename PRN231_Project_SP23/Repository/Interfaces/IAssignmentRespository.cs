using BusinessObjects.Models;
using BusinessObjects.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAssignmentRespository
    {
        void SaveAssignment(UploadAssignmentViewModel uploadAssignmentViewModel);
        IEnumerable<Assignment> GetAssignmentsByCourseId(int courseId);
        IEnumerable<Assignment> ListAssignmentByTeacherAndCourse(int teacherId, int courseId);
        Assignment GetAssignmentsByAssId(int assId);

    }
}
