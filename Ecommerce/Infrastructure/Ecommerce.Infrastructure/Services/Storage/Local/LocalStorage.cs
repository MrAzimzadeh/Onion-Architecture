using Ecomerce.Infrastructure.StaticService;
using Ecommerce.Application.Abstractions.Storeg.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorege
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }


    public async Task DeleteAsync(string path, string fileName)
        => File.Delete($"{path}\\{fileName}");

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new(path);
        return directory.GetFiles().Select(f => f.Name).ToList();
    }

    public bool HasFile(string path, string fileName)
        => File.Exists($"{path}\\{fileName}");

    private async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            // Generate a new sanitized file name
            string newFileName = await GenerateNewFileNameAsync(Path.GetDirectoryName(path), file.FileName);

            // Combine the path with the new file name
            string newPath = Path.Combine(Path.GetDirectoryName(path), newFileName);

            await using FileStream fileStream = new(newPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception ex)
        {
            //todo log!
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


    public async Task<List<(string fileName, string pathOrContainer)>> UploadRangeAsync(string pathOrContainer,
        IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainer);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
            datas.Add((file.Name, $"{pathOrContainer}\\{file.Name}"));
        }

        return datas;
    }
}