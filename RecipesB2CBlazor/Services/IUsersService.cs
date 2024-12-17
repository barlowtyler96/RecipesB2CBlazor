using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Services;

public interface IUsersService
{
    Task<bool> AddUserFavoriteAsync(int recipeId);
    Task<bool> DeleteUserFavoriteAsync(int recipeId);
    Task<List<Recipe>> GetUserCreatedRecipesAsync();
    Task<List<int>> GetUserFavoritesAsync();
    Task<List<Recipe>> GetUserFavoriteRecipesAsync();
    Task<bool> AddNewUserAsync();   
}