using System.Net;
using Microsoft.AspNetCore.Http;
using Priyosaj.Service.Interfaces;

namespace Priyosaj.Service
{
    public class FileUploadService: IFileUploadService
    {
        

        public async Task<List<string>> UploadFiles(string path, IFormFileCollection files)
        {
            List<string> filePaths = new List<string>();
            try{
                foreach (var file in files)
                {
                    var fileName = Path.Combine(path, file.FileName);
                    using (var fileStream = System.IO.File.Create(fileName))
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
}