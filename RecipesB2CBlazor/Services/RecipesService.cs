using Microsoft.Identity.Web;
using RecipesB2CBlazor.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace RecipesB2CBlazor.Services;

public static class RecipesServiceExtensions
{
    public static void AddRecipesService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IRecipesService, RecipesService>();
    }
}
public class RecipesService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpClient _httpClient;
    private readonly string _RecipesScope = string.Empty;
    private readonly string _RecipesBaseAddress = string.Empty;
    private readonly ITokenAcquisition _tokenAcquisition;

    public RecipesService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, 
        IConfiguration configuration, IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _tokenAcquisition = tokenAcquisition;
        _contextAccessor = contextAccessor;
        _RecipesScope = configuration["DownstreamApi:Scopes"];
        _RecipesBaseAddress = configuration["DownstreamApi:BaseUrl"];
    }

    public async Task<RecipeModel> AddAsync(RecipeModel recipeModel)
    {
        await PrepareAuthenticatedClient();
    }

    private async Task PrepareAuthenticatedCLient()
    {
        var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _RecipesScope });
        Debug.WriteLine($"access token - {accessToken}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    }
}
