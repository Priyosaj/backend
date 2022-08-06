using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Priyosaj.Core.Entities;
using Priyosaj.Core.Interfaces.Repositories;
using Priyosaj.Core.Interfaces.Services;

namespace Priyosaj.Service;

public class FileUploadService : IFileUploadService
{
    private readonly IUnitOfWork _unitOfWork;
    public FileUploadService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // public async Task<List<string>> UploadFiles(string path, IFormFileCollection files)
    // {
    //     List<string> filePaths = new List<string>();
    //     try{
    //         foreach (var file in files)
    //         {
    //             var fileName = Path.Combine(path, file.FileName);
    //             await using (var fileStream = File.Create(fileName))
    //             {
    //                 await file.CopyToAsync(fileStream);
    //                 fileStream.Flush();
    //             }
    //             filePaths.Add(fileName.Split("wwwroot")[1]);
    //         }
    //         return filePaths;
    //     }
    //     catch(Exception ex)
    //     {
    //         return null;
    //     }
    // }

    public async Task<List<FileEntity>> UploadFiles(string keyPrefix, string rootPath, IFormFileCollection files)
    {
        List<FileEntity> fileEntities = new List<FileEntity>();
        Console.WriteLine(rootPath);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        var dirToFiles = Path.Combine(rootPath, keyPrefix);
        if (!Directory.Exists(dirToFiles))
        {
            Directory.CreateDirectory(dirToFiles);
        }

        try
        {
            foreach (var file in files)
            {
                var fileName = $"{keyPrefix}_{Guid.NewGuid()}_{file.FileName}";
                Console.WriteLine(fileName);
                var newFile = new FileEntity
                {
                    Name = fileName,
                    Type = FileType.Image,
                    Url = Path.Combine(keyPrefix, fileName)
                };
                await using (var fileStream = File.Create(Path.Combine(rootPath, newFile.Url)))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Flush();
                }
                fileEntities.Add(newFile);
                _unitOfWork.Repository<FileEntity>().Add(newFile);
                await _unitOfWork.Complete();
            }
            return fileEntities;
        }
        catch (Exception ex)
        {
            foreach (var file in fileEntities)
            {
                File.Delete(Path.Combine(rootPath, file.Url));
            }
            throw ex;
        }
    }

}