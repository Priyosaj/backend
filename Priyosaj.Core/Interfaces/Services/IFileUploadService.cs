using Microsoft.AspNetCore.Http;

namespace Priyosaj.Core.Interfaces.Services;

public interface IFileUploadService
{
    Task<List<string>> UploadFiles(string path, IFormFileCollection files);
}