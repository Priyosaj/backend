using Microsoft.AspNetCore.Http;
using Priyosaj.Core.Interfaces.Services;

namespace Priyosaj.Service;

public class FileUploadService: IFileUploadService
{
    public async Task<List<string>> UploadFiles(string path, IFormFileCollection files)
    {
        List<string> filePaths = new List<string>();
        try{
            foreach (var file in files)
            {
                var fileName = Path.Combine(path, file.FileName);
                await using (var fileStream = File.Create(fileName))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Flush();
                }
                filePaths.Add(fileName.Split("wwwroot")[1]);
            }
            return filePaths;
        }
        catch(Exception ex)
        {
            return null;
        }
    }
}