﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using RecipesB2CBlazor.Helpers;
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
    private readonly HttpClient _httpClient;
    private readonly string _recipesScope = string.Empty;
    private readonly string _recipesBaseAddress = string.Empty;
    private readonly string _recipesTenantId = string.Empty;
    private readonly ITokenAcquisition _tokenAcquisition;

    public RecipesService(ITokenAcquisition tokenAcquisition, HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _tokenAcquisition = tokenAcquisition;
        _recipesScope = configuration["DownstreamApi:Scopes"];
        _recipesTenantId = configuration["AzureAd:TenantId"];
        _recipesBaseAddress = configuration["DownstreamApi:BaseUrl"];
    }

    public async Task<bool> AddAsync(RecipeDto recipeDto)
    {
        await PrepareAuthenticatedClientForApp();

        var jsonRequest = JsonConvert.SerializeObject(recipeDto); //put both these into 

        var jsonContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await this._httpClient.PostAsync($"{_recipesBaseAddress}Administrator", jsonContent);

        if (response.StatusCode == HttpStatusCode.OK)
        {

            return response.IsSuccessStatusCode;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task DeleteAsync(int id)
    {
        await PrepareAuthenticatedClientForApp();

        var response = await this._httpClient.DeleteAsync($"{_recipesBaseAddress}Recipes/{id}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return;
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    public async Task<RecipeModel> EditAsync(RecipeModel recipeModel)
    {
        await PrepareAuthenticatedClientForApp();

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
        await PrepareAuthenticatedClientForApp();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<RecipeModel> recipeList = JsonConvert.DeserializeObject<IEnumerable<RecipeModel>>(content);

            return recipeList.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<List<RecipeDto>> GetRecentsAsync()
    {
        await PrepareAuthenticatedClientForApp();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes/recent");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<RecipeDto> recipeDtoList = JsonConvert.DeserializeObject<IEnumerable<RecipeDto>>(content);

            return recipeDtoList.ToList();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<RecipeDto> GetAsync(int id)
    {
        await PrepareAuthenticatedClientForApp();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes/id/{id}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<IEnumerable<RecipeDto>>(content);

            return recipes.FirstOrDefault();
        }
        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}");
    }

    public async Task<RecipesResponse> GetByKeyword(string keyword, int currentPageNumber, int pageSize)
    {
        await PrepareAuthenticatedClientForApp();

        var response = await _httpClient.GetAsync($"{_recipesBaseAddress}Recipes/keyword={keyword}/page={currentPageNumber}/pageSize={pageSize}");

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
        var accessToken = await _tokenAcquisition.GetAccessTokenForAppAsync(_recipesScope, JwtBearerDefaults.AuthenticationScheme, tenant: _recipesTenantId);
        Debug.WriteLine($"access token - {accessToken}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}
