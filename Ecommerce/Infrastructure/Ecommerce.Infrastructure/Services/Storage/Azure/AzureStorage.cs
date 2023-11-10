using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Ecomerce.Application.Abstractions.Storeg.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Ecomerce.Infrastructure.Services.Storage.Azure;

public class AzureStorage : IAzureStorage

{
    readonly BlobServiceClient _blobServiceClient; // Qosulmaq ucun istifade olunur
    BlobContainerClient _blobContainerClient; // 

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["Storage:AzureConnectionString"]);
    }


    public async Task<List<(string fileName, string pathOrContainer)>> UploadRangeAsync(string containerName,
        IFormFileCollection files)
    {
        _blobContainerClient =
            _blobServiceClient
                .GetBlobContainerClient(
                    containerName); // Containeri aliriq yeni hansi container uzerinde isleyecekse onu aliriq 

        await _blobContainerClient.CreateIfNotExistsAsync(); // Eger bele bir container yoxdursa yaradir

        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType
            .BlobContainer); //  icindeki butun fayllari oxuna bilir edir 

        List<(string fileName, string pathOrContainer)> datas = new(); // List 

        foreach (IFormFile file in files)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(file.Name); // faylin adini aliriq
            await blobClient.UploadAsync(file.OpenReadStream()); // fayli yukleyirik
            datas.Add((file.Name, containerName));
        }

        return datas;
    }

    public async Task DeleteAsync(string container, string fileName)
    {
        _blobContainerClient =
            _blobServiceClient.GetBlobContainerClient(
                blobContainerName: container); // Containeri aliriq yeni hansi container uzerinde isleyecekse onu aliriq
        BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName: fileName); // faylin adini aliriq
        await blobClient.DeleteAsync(); // fayli silirik
    }

    public List<string> GetFiles(string container)
    {
        _blobContainerClient =
            _blobServiceClient.GetBlobContainerClient(
                blobContainerName: container); // Containeri aliriq yeni hansi container uzerinde isleyecekse onu aliriq
        List<string> datas = new();
        foreach (BlobItem blobItem in _blobContainerClient.GetBlobs()) // containerin icindeki butun fayllari aliriq 
        {
            datas.Add(blobItem.Name);
        }

        return datas;
    }

    public bool HasFile(string container, string fileName)
    {
        _blobContainerClient =
            _blobServiceClient.GetBlobContainerClient(
                blobContainerName: container); // Containeri aliriq yeni hansi container uzerinde isleyecekse onu aliriq
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }
}