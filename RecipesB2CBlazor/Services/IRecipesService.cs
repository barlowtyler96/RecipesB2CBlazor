﻿using RecipesB2CBlazor.Helpers;
using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Services
{
    public interface IRecipesService
    {
        Task<RecipeModel> AddAsync(RecipeModel recipeModel);
        Task DeleteAsync(int id);
        Task<RecipeModel> EditAsync(RecipeModel recipeModel);
        Task<List<RecipeModel>> GetAllAsync();
        Task<RecipeModel> GetAsync(int id);
        Task<RecipesResponse> GetByKeyword(string keyword, int currentPageNumber, int pageSize);
        Task<List<RecipeModel>> GetRecentsAsync();
    }
}