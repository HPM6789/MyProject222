using BusinessObjects.Models;
using BusinessObjects.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SubmitAssignmentDao
    {
        public static void SubmitAssignment(SubmitAssignmentViewModel model)
        {
            String path = addAssFileToAPILocal(model);
            model.Path = path;
            //add record to db

            //data access
            using (var context = new PRN231_ProjectContext())
            {
                try
                {
                    SubmitAssignment sbAss = new SubmitAssignment
                    {
                        SubmitAssignmentName = model.SubmitAssignmentName,
                        UploaderId = model.UploaderId,
                        Path = model.Path,
                        AssignmentId = model.AssignmentId,
                        Description = model.Description,
                    };
                    context.SubmitAssignments.Add(sbAss);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private static String addAssFileToAPILocal(SubmitAssignmentViewModel model)
        {
            model.IsResponse = true;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AllFiles/SubmitAssignment");
            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //get file extension
            FileInfo fileInfo = new FileInfo(model.SubmitFile.FileName);
            //set file name TeacherID_CourseID_FileName
            string fileName = model.UploaderId + "_" + model.AssignmentId + "_" + model.SubmitFile.FileName;
            //model.FileName + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                model.SubmitFile.CopyTo(stream);
            }
            model.IsSuccess = true;
            model.Message = "File upload successfully";
            return fileNameWithPath;
        }
    }
}
