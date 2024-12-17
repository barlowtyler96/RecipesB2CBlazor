using RecipesB2CBlazor.Helpers;
using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Services;

public interface IRecipesService
{
    Task<int> AddAsync(Recipe recipeDto);
    Task DeleteAsync(int id);
    Task<Recipe> EditAsync(Recipe recipeModel);
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe> GetAsync(int id);
    Task<RecipesResponse> GetByKeyword(string keyword, int currentPageNumber, int pageSize);
    Task<RecipesResponse> GetRecentsAsync(int page, int pageSize);
}