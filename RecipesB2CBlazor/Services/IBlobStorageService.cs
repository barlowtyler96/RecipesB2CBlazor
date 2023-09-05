namespace RecipesB2CBlazor.Services;
public interface IBlobStorageService
{
    Task<string> UploadFileToBlobAsync(string strFileName, string contecntType, Stream fileStream);
}