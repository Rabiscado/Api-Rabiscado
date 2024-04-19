using Azure.Storage.Blobs;

namespace Rabiscado.Core.Extensions;

public static class BlobExtensions
{
    public static BlobContainerClient GetContainer(string connectionString, string containerName)
    {
        BlobServiceClient blobServiceClient = new(connectionString);
        return blobServiceClient.GetBlobContainerClient(containerName);
    }
}