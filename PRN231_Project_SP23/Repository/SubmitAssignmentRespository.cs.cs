﻿using BusinessObjects.Models;
using BusinessObjects.ViewModel;
using DataAccess;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SubmitAssignmentRespository : ISubmitAssignmentRespository
    {
        public SubmitAssignment GetSubmitAssignmentsById(int id)
        => SubmitAssignmentDao.GetSubmitAssignmentsById(id);

        public IEnumerable<SubmitAssignment> ListSubmitAssignmentByAssId(int assId)
         => SubmitAssignmentDao.ListSubmitAssignmentByAssId(assId);

        public void SubmitAssignment(SubmitAssignmentViewModel model)
         => SubmitAssignmentDao.SubmitAssignment(model);
    }
}
