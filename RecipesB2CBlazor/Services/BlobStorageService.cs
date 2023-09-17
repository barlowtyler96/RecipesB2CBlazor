using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace RecipesB2CBlazor.Services;
public class BlobStorageService : IBlobStorageService
{
    private readonly IConfiguration _configuration;
    string blobStorageconnection = string.Empty;
    private string blobContainerName = "recipevaultimages";
    public BlobStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
        blobStorageconnection = _configuration.GetConnectionString("AzureStorageAccount");
    }
    public async Task<string> UploadFileToBlobAsync(string strFileName, string contentType, Stream fileStream)
    {
        try
        {
            var container = new BlobContainerClient(blobStorageconnection, blobContainerName);
            var createResponse = await container.CreateIfNotExistsAsync();
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var blob = container.GetBlobClient(strFileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
            var urlString = blob.Uri.ToString();
            return urlString;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> DeleteFileToBlobAsync(string strFileName)
    {
        try
        {
            var container = new BlobContainerClient(blobStorageconnection, blobContainerName);
            var createResponse = await container.CreateIfNotExistsAsync();
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var blob = container.GetBlobClient(strFileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            return true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}