using BusinessObjects.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
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

        public void SaveMaterial(IFormFile material, string materialPath, int courseId, int uploaderId, string materialName) 
            => MaterialDao.SaveMaterial(material, materialPath, courseId, uploaderId, materialName);
    }
}
