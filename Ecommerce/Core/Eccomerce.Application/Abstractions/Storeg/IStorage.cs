using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Abstractions.Storeg;

public interface IStorage
{
    Task<List<(string fileName, string pathOrContainer)>> UploadRangeAsync(string pathOrContainer,
        IFormFileCollection files);

    Task DeleteAsync(string pathOrContainer, string fileName);
    List<string> GetFiles(string pathOrContainer);
    bool HasFile(string pathOrContainer, string fileName);
}