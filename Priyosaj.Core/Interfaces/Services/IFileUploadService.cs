using Microsoft.AspNetCore.Http;
using Priyosaj.Core.Entities;

namespace Priyosaj.Core.Interfaces.Services;

public interface IFileUploadService
{
    // Task<List<string>> UploadFiles(string path, IFormFileCollection files);
    Task<List<FileEntity>> UploadFiles(string keyPrefix, string rootPath, IFormFileCollection files);
}