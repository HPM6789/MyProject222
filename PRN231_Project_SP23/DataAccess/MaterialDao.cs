using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MaterialDao
    {
        public static IEnumerable<Material> GetMaterialsByCourseId(int courseId)
        {
            List<Material> list = new List<Material>();
            try
            {
                using (var context = new PRN231_ProjectContext())
                {
                    list = context.Materials.Where(a => a.CourseId == courseId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
