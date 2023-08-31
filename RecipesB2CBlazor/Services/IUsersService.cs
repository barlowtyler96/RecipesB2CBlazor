using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Services;

public interface IUsersService
{
    Task<bool> AddUserFavoriteAsync(int recipeId);
    Task<bool> DeleteUserFavoriteAsync(int recipeId);
    Task<List<UserFavorite>> GetUserFavoriteIdsAsync();
}