using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecomerce.Infrastructure.StaticService;
using Ecommerce.Application.Abstractions.Storeg.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorege
{
    public readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<(string fileName, string pathOrContainer)>> UploadRangeAsync(string pathOrContainer,
        IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainer);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            // string fileNewName = await GenerateNewFileNameAsync(uploadPath, file.FileName);
            bool result = await CopyFileAsync(Path.Combine(uploadPath, file.Name), file);
            datas.Add((file.Name, Path.Combine(pathOrContainer, file.Name)));
        }

        return datas;
    }


    public async Task DeleteAsync(string pathOrContainer, string fileName)
        => File.Delete($"{pathOrContainer}\\{fileName}");


    public List<string> GetFiles(string pathOrContainer)
    {
        DirectoryInfo directoryInfo = new(pathOrContainer);
        return directoryInfo.GetFiles().Select(f => f.Name).ToList();
    }

    public bool HasFile(string pathOrContainer, string fileName)
        => File.Exists($"{pathOrContainer}\\{fileName}");


    // private 
    public async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024,
                useAsync: false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw ex;
        }
    }

    private async Task<string> GenerateNewFileNameAsync(string path, string fileName)
    {
        string extension = Path.GetExtension(fileName);
        string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        string baseFileName = NameService.CharacterRegulatory(nameWithoutExtension);
        string newFileName = $"{baseFileName}{extension}";

        int index = 2;

        while (File.Exists(Path.Combine(path, newFileName)))
        {
            newFileName = $"{baseFileName}-{index++}{extension}";
        }

        return newFileName;
    }
}