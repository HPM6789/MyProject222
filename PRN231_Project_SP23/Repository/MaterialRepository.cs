using BusinessObjects.Models;
using DataAccess;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        public IEnumerable<Material> GetMaterialsByCourseId(int courseId) => MaterialDao.GetMaterialsByCourseId(courseId);
    }
}
