using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Ecomerce.Application.Abstractions.Storeg.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Ecomerce.Infrastructure.Services.Storage.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    readonly BlobServiceClient _blobServiceClient; // Qoşulmaq üçün istifadə olunur
    BlobContainerClient _blobContainerClient; // Blob konteynerinə çatmaq üçün istifadə olunur

    public AzureStorage(IConfiguration configuration)
    {
        // Azure Storage bağlantı cümləsindən istifadə edərək Blob xidməti müştərisini yaradır
        _blobServiceClient = new BlobServiceClient(configuration["Storage:AzureConnectionString"]);
    }

    // Bir çox faylları yükləmək üçün istifadə olunan metot

    // Faylı silmək üçün istifadə olunan metot


    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync(); // Eğer konteyner yoxdursa yarat 
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType
            .BlobContainer); // Konteyner üçün Acces falan.. 

        List<(string fileName, string pathOrContainerName)> datas = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(containerName, file.FileName, HasFile);
            // string error = await FileRenameAsync(containerName, file.Name, HasFile);

            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
            await blobClient.UploadAsync(file.OpenReadStream()); // Faylı yüklə
            datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
        }

        return datas;
    }

    public async Task DeleteAsync(string container, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync(); // Faylı sil
    }

    // Konteynerdəki bütün faylları almaq üçün istifadə olunan metot
    public List<string> GetFiles(string container)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
        List<string> datas = new();
        foreach (BlobItem blobItem in _blobContainerClient.GetBlobs())
        {
            datas.Add(blobItem.Name);
        }

        return datas;
    }

    // Faylın mövcud olub olmadığını yoxlamaq üçün istifadə olunan metot
    public bool HasFile(string container, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }
}