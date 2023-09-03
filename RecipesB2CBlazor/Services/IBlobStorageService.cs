namespace RecipesB2CBlazor.Services;
public interface IBlobStorageService
{
    Task<bool> DeleteFileToBlobAsync(string strFileName);
    Task<string> UploadFileToBlobAsync(string strFileName, string contecntType, Stream fileStream);
}