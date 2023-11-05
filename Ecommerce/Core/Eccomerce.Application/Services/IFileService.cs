using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace Ecomerce.Application.Services;

public interface IFileService
{
    void Upload(string path, IFormFileCollection files);
    Task UploadAsync(string path, IFormFileCollection files);

    Task<string> FileRenameAsync(string fileName);

    string FileRename();
    
    Task<bool> CopyFileAsync(string path, IFormFile file);
    bool CopyFile(string path, IFormFile file);
    
}