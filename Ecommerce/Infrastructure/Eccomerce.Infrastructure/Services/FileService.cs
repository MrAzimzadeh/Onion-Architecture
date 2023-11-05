using System.Runtime.CompilerServices;
using Ecomerce.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Eccomerce.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public void Upload(string path, IFormFileCollection files)
    {
        throw new NotImplementedException();
    }

    public async Task UploadAsync(string path, IFormFileCollection files)
    {
        // Ozu tapir 
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(file.FileName);
            await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
        }
    }

    public Task<string> FileRenameAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public string FileRename()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024,
                useAsync: false);
            await fileStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception e)
        {
            // todo  log
            Console.WriteLine(e);
            throw;
        }
    }

    public bool CopyFile(string path, IFormFile file)
    {
        throw new NotImplementedException();
    }
}