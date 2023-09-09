using RecipesB2CBlazor.Helpers;
using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Services;

public interface IRecipesService
{
    Task<int> AddAsync(RecipeModel recipeDto);
    Task DeleteAsync(int id);
    Task<RecipeDto> EditAsync(RecipeDto recipeModel);
    Task<List<RecipeDto>> GetAllAsync();
    Task<RecipeModel> GetAsync(int id);
    Task<RecipesResponse> GetByKeyword(string keyword, int currentPageNumber, int pageSize);
    Task<RecipesResponse> GetRecentsAsync(int page, int pageSize);
}