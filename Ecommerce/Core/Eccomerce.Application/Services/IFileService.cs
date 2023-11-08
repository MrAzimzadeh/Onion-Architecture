using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace Ecomerce.Application.Services;

public interface IFileService
{
    Task<List<(string fileName, string path)>> UploadRangeAsync(string path, IFormFileCollection files);
    Task<bool> CopyFileAsync(string path, IFormFile file);

    Task<(string fileName, string path)> UploadAsync(string path, IFormFile file);
    (string fileName, string path) Upload(string path, IFormFile file);
    
}