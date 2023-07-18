﻿using Microsoft.Identity.Web;
using Newtonsoft.Json;
using RecipesB2CBlazor.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace RecipesB2CBlazor.Services;

public static class RecipesServiceExtensions
{
    public static void AddRecipesService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IRecipesService, RecipesService>();
    }
}
public class RecipesService : IRecipesService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpClient _httpClient;
    private readonly string _recipesScope = string.Empty;
    private readonly string _recipesBaseAddress = string.Empty;
    private readonly ITokenAcquisition _tokenAcquisition;

    public RecipesService(ITokenAcquisition tokenAcquisition, HttpClient httpClient,
        IConfiguration configuration, IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _tokenAcquisition = tokenAcquisition;
        _contextAccessor = contextAccessor;
        _recipesScope = configuration["DownstreamApi:Scopes"];
        _recipesBaseAddress = configuration["DownstreamApi:BaseUrl"];
    }

    public async Task<RecipeModel> AddAsync(RecipeModel recipeModel)
    {
        await PrepareAuthenticatedClient();

        var jsonRequest = JsonConvert.SerializeObject(recipeModel);
        var jsonContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await this._httpClient.PostAsync($"{_recipesBaseAddress}Administrator", jsonContent);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            recipeModel = JsonConvert.DeserializeObject<RecipeModel>(content);

            return recipeModel;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task DeleteAsync(int id)
    {
        await PrepareAuthenticatedClient();

        var response = await this._httpClient.DeleteAsync($"{_recipesBaseAddress}Recipes/{id}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task<RecipeModel> EditAsync(RecipeModel recipeModel)
    {
        await PrepareAuthenticatedClient();

        var jsonRequest = JsonConvert.SerializeObject(recipeModel);
        var jsonContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_recipesBaseAddress}Administrator/{recipeModel.Id}", jsonContent);


        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            recipeModel = JsonConvert.DeserializeObject<RecipeModel>(content);

            return recipeModel;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task<List<RecipeModel>> GetAllAsync()
    {
        await PrepareAuthenticatedClient();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<RecipeModel> recipeList = JsonConvert.DeserializeObject<IEnumerable<RecipeModel>>(content);

            return recipeList.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<List<RecipeModel>> GetRecentsAsync()
    {
        await PrepareAuthenticatedClient();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes/recent");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<RecipeModel> recipeList = JsonConvert.DeserializeObject<IEnumerable<RecipeModel>>(content);

            return recipeList.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<RecipeModel> GetAsync(int id)
    {
        await PrepareAuthenticatedClient();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes/id/{id}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            RecipeModel recipe = JsonConvert.DeserializeObject<RecipeModel>(content);

            return recipe;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<List<RecipeModel>> GetByKeyword(string keyword)
    {
        await PrepareAuthenticatedClient();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes/keyword/{keyword}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<RecipeModel> recipes = JsonConvert.DeserializeObject<IEnumerable<RecipeModel>>(content);

            return recipes.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    private async Task PrepareAuthenticatedClient()
    {
        var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _recipesScope });
        Debug.WriteLine($"access token - {accessToken}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}
