using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Extensions;
using Rabiscado.Core.Settings;

namespace Rabiscado.Application.Services;

public class FileService : BaseService, IFileService
{
    private readonly AzureStorageSettings _azureStorageSettings;
    public FileService(IMapper mapper, INotificator notificator, IOptions<AzureStorageSettings> azureStorageSettings) : base(mapper, notificator)
    {
        _azureStorageSettings = azureStorageSettings.Value;
    }

    public async Task<List<string>> GetAllDocuments()
    {
        var blobs = new List<string>();
        var container = BlobExtensions.GetContainer(_azureStorageSettings.ConnectionString, _azureStorageSettings.ContainerName);
        if (!await container.ExistsAsync())
        {
            return new List<string>();
        }
        
        await foreach (var blobItem in container.GetBlobsAsync())
        {
            blobs.Add(blobItem.Name);
        }
        return blobs;
    }

    public async Task<Stream?> GetDocument(string fileName)
    {
        var container = BlobExtensions.GetContainer(_azureStorageSettings.ConnectionString, _azureStorageSettings.ContainerName);
        if (await container.ExistsAsync())
        {
            var blobClient = container.GetBlobClient(fileName);
            if (!await blobClient.ExistsAsync())
            {
                Notificator.Handle("BlobClient not found");
                return null;
            }
            var content = await blobClient.DownloadStreamingAsync();
            return content.Value.Content;
        }
        
        Notificator.Handle("BlobContainer not found");
        return null;
    }

    public async Task<string?> UploadFile(IFormFile file)
    {
        var type = file.FileName.Substring(file.FileName.LastIndexOf('.'));
        var blobName = Guid.NewGuid() + type;

        var blobServiceClient = new BlobServiceClient(_azureStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageSettings.ContainerName);
        var blobClient = containerClient.GetBlobClient(blobName);


        var blobHttpHeader = new BlobHttpHeaders { ContentType = file.ContentType };


        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
        return $"{_azureStorageSettings.BaseUrl}/{blobName}";
    }

    public async Task<string?> UploadFile(MemoryStream file)
    {
        var blobName = Guid.NewGuid().ToString();
        
        var blobServiceClient = new BlobServiceClient(_azureStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageSettings.ContainerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        var blobHttpHeader = new BlobHttpHeaders { ContentType = "application/pdf" };
        file.Seek(0, SeekOrigin.Begin);
        await blobClient.UploadAsync(file, new BlobUploadOptions { HttpHeaders = blobHttpHeader });

        return $"{_azureStorageSettings.BaseUrl}/{blobName}";
    }

    public async Task<bool> DeleteDocument(string fileName)
    {
        var container = BlobExtensions.GetContainer(_azureStorageSettings.ConnectionString, _azureStorageSettings.ContainerName);
        if (!await container.ExistsAsync())
        {
            return false;
        }

        var blobClient = container.GetBlobClient(fileName);
        if (!await blobClient.ExistsAsync()) return false;
        await blobClient.DeleteIfExistsAsync();
        return true;
    }
}