using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Application.Abstractions.Storeg;
using Microsoft.AspNetCore.Http;

namespace Ecomerce.Infrastructure.Services.Storage;

public class StorageService : IStorageService
{
    private readonly IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public Task<List<(string fileName, string pathOrContainer)>> UploadRangeAsync(string pathOrContainer,
        IFormFileCollection files)
        => _storage.UploadRangeAsync(pathOrContainer: pathOrContainer, files: files);

    public async Task DeleteAsync(string pathOrContainer, string fileName)
        => await _storage.DeleteAsync(pathOrContainer, fileName);


    public List<string> GetFiles(string pathOrContainer)
        => _storage.GetFiles(pathOrContainer);

    public bool HasFile(string pathOrContainer, string fileName)
        => _storage.HasFile(pathOrContainer, fileName);

    public string StorageName
    {
        get => _storage.GetType().Name;
    }
    
}