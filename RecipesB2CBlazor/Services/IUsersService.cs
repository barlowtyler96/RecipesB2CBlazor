using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Services;

public interface IUsersService
{
    Task<bool> AddUserFavoriteAsync(int recipeId);
    Task<List<RecipeDto>> GetUserCreatedRecipesAsync();
    Task<bool> DeleteUserFavoriteAsync(int recipeId);
    Task<List<RecipeDto>> GetUsersFavoritesAsync();
    Task<List<int>> GetUserFavoriteIdsAsync();
    Task<bool> AddNewUserAsync();   
}