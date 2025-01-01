using RecipesB2CBlazor.Helpers;
using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Services;

public interface IRecipesService
{
    Task<Recipe> AddAsync(Recipe recipeDto);
    Task DeleteAsync(int id);
    Task<Recipe> EditAsync(Recipe recipeModel);
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe> GetByIdAsync(int id);
    Task<PaginationResponse<Recipe>> GetByKeyword(string keyword, int currentPageNumber, int pageSize);
    Task<PaginationResponse<Recipe>> GetRecentsAsync(int page, int pageSize);
}