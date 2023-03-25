using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IMaterialRepository
    {
        IEnumerable<Material> GetMaterialsByCourseId(int courseId);
        void SaveMaterial(IFormFile material, string materialPath, int courseId, int uploaderId, string materialName);
    }
}
