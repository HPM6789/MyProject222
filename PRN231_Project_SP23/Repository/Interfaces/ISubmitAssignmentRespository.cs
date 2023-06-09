﻿using BusinessObjects.Models;
using BusinessObjects.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
     public interface ISubmitAssignmentRespository
    {
        void SubmitAssignment(SubmitAssignmentViewModel submitAssignmentViewModel);
        IEnumerable<SubmitAssignment> ListSubmitAssignmentByAssId(int assId);
        SubmitAssignment GetSubmitAssignmentsById(int id);
    }
}
