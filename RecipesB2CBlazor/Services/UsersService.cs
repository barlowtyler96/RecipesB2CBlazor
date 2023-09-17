using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using RecipesB2CBlazor.Models;
using System.Net;

namespace RecipesB2CBlazor.Services;

public static class UsersServiceExtensions
{
    public static void AddUsersService(this IServiceCollection services)
    {
        services.AddHttpClient<IUsersService, UsersService>();
    }
}
public class UsersService : IUsersService
{
    private readonly HttpClient _httpClient;
    private readonly string _usersScope = string.Empty;
    private readonly string _usersBaseAddress = string.Empty;
    private readonly ITokenAcquisition _tokenAcquisition;

    public UsersService(ITokenAcquisition tokenAcquisition, HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _tokenAcquisition = tokenAcquisition;
        _usersScope = configuration["DownstreamApi:Scopes"];
        _usersBaseAddress = configuration["DownstreamApi:BaseUrl"];
    }

    public async Task<bool> AddNewUserAsync()
    {
        await PrepareAuthenticatedClientForUser();

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_usersBaseAddress}Users/new");

        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return response.IsSuccessStatusCode;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task<List<RecipeDto>> GetUserCreatedRecipesAsync()
    {
        await PrepareAuthenticatedClientForUser();

        var response = await _httpClient.GetAsync($"{_usersBaseAddress}Users/myrecipes");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<RecipeDto> userCreatedRecipes = JsonConvert.DeserializeObject<IEnumerable<RecipeDto>>(content)!;

            return userCreatedRecipes!.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<bool> AddUserFavoriteAsync(int recipeId)
    {
        await PrepareAuthenticatedClientForUser();

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_usersBaseAddress}Users/favorite/{recipeId}");

        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return response.IsSuccessStatusCode;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task<bool> DeleteUserFavoriteAsync(int recipeId)
    {
        await PrepareAuthenticatedClientForUser();

        var response = await _httpClient.DeleteAsync($"{_usersBaseAddress}Users/favorite/{recipeId}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return response.IsSuccessStatusCode;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task<List<RecipeDto>> GetUsersFavoritesAsync()
    {
        await PrepareAuthenticatedClientForUser();

        var response = await _httpClient.GetAsync($"{_usersBaseAddress}Users/favorites");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<RecipeDto> userFavorites = JsonConvert.DeserializeObject<IEnumerable<RecipeDto>>(content);

            return userFavorites.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<List<int>> GetUserFavoriteIdsAsync()
    {
        await PrepareAuthenticatedClientForUser();

        var response = await _httpClient.GetAsync($"{_usersBaseAddress}Users/favoritesIds");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<int> userFavorites = JsonConvert.DeserializeObject<IEnumerable<int>>(content);

            return userFavorites.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    private async Task PrepareAuthenticatedClientForUser()
    {
        var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _usersScope });
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}
