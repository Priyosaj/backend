using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Priyosaj.Service.Interfaces
{
    public interface IFileUploadService
    {
        Task<List<string>> UploadFiles(string path, IFormFileCollection files);
    }
}