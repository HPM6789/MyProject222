using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.ViewModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AssignmentRepository : IAssignmentRespository
    {
        //save file to api, save record to database
        public void SaveAssignment(UploadAssignmentViewModel model)
        {
            String filePath = addAssFileToAPILocal(model);
            //add record to db
            //data access
            using (var context = new PRN231_ProjectContext())
            {
                    try
                    {
                        Assignment ass = new Assignment
                        {
                            CourseId = model.CourseId,
                            UploaderId = model.UploaderId,
                            Path = filePath,
                            AssignmentName = model.AssignmentName,
                            RequiredDate = model.RequiredDate,
                        };
                    context.Assignments.Add(ass);
                    context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                
            }
        }
        private String addAssFileToAPILocal(UploadAssignmentViewModel model)
        {
            model.IsResponse = true;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AllFiles/Assigments");
            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //get file extension
            FileInfo fileInfo = new FileInfo(model.Assignment.FileName);
            //set file name TeacherID_CourseID_FileName
            string fileName = model.UploaderId + "_" + model.CourseId + "_" + model.Assignment.FileName;
            //model.FileName + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                model.Assignment.CopyTo(stream);
            }
            model.IsSuccess = true;
            model.Message = "File upload successfully";
            return fileNameWithPath;
        }
    }
}
