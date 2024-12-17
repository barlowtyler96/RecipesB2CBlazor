using Microsoft.Identity.Web;
using Newtonsoft.Json;
using RecipesB2CBlazor.Helpers;
using RecipesB2CBlazor.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace RecipesB2CBlazor.Services;

public static class RecipesServiceExtensions
{
    public static void AddRecipesService(this IServiceCollection services)
    {
        services.AddHttpClient<IRecipesService, RecipesService>();
    }
}
public class RecipesService : IRecipesService
{
    private readonly string _usersScope;
    private readonly HttpClient _httpClient;
    private readonly ITokenAcquisition _tokenAcquisition;
    private readonly string _recipesBaseAddress = string.Empty;
    

    public RecipesService(HttpClient httpClient,
        IConfiguration configuration, ITokenAcquisition tokenAcquisition)
    {
        _usersScope = configuration["DownstreamApi:Scopes"];
        _httpClient = httpClient;
        _tokenAcquisition = tokenAcquisition;
        _recipesBaseAddress = configuration["DownstreamApi:BaseUrl"];
    }

    public async Task<int> AddAsync(Recipe recipeModel)
    {
        await PrepareAuthenticatedClientForApp();

        var jsonRequest = JsonConvert.SerializeObject(recipeModel);
        var jsonContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_recipesBaseAddress}Recipes/share", jsonContent);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var createdRecipeId = JsonConvert.DeserializeObject<int>(responseContent);

            return createdRecipeId;
        }

        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task DeleteAsync(int id)
    {
        await PrepareAuthenticatedClientForApp();

        var response = await _httpClient.DeleteAsync($"{_recipesBaseAddress}Recipes/{id}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task<Recipe> EditAsync(Recipe recipeModel)
    {
        await PrepareAuthenticatedClientForApp();

        var jsonRequest = JsonConvert.SerializeObject(recipeModel);
        var jsonContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_recipesBaseAddress}Administrator/{recipeModel.Id}", jsonContent);


        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            recipeModel = JsonConvert.DeserializeObject<Recipe>(content);

            return recipeModel;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        await PrepareAuthenticatedClientForApp();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<Recipe> recipeList = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(content);

            return recipeList.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }
    public async Task<RecipesResponse> GetRecentsAsync(int page, int pageSize)
    {
        string uri = _recipesBaseAddress + "Recipes" + "?page=" + page + "&pageSize=" + pageSize;

        var response = await _httpClient.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();

            var recipesResponse = JsonConvert.DeserializeObject<RecipesResponse>(content);

            return recipesResponse;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<Recipe> GetAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes/{id}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            var recipe = JsonConvert.DeserializeObject<Recipe>(content);

            return recipe;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<RecipesResponse> GetByKeyword(string keyword, int page, int pageSize)
    {
        string uri = _recipesBaseAddress + "Recipes/search" + "?keyword=" + WebUtility.UrlEncode(keyword) + "&page=" + page + "&pageSize=" + pageSize;

        var response = await _httpClient.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            var recipesResponse = JsonConvert.DeserializeObject<RecipesResponse>(content);

            return recipesResponse;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    private async Task PrepareAuthenticatedClientForApp()
    {
        var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _usersScope });
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}
