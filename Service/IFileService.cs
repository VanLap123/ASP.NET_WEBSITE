using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBGROUP_GCC0903.Service
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);
        bool DeleteImage(string imageFileName);
    }
}