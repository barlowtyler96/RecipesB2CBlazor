using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Helpers;

public class RecipesResponse
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; } = 8;

    public int CurrentPageNumber { get; set; } = 1;

    public List<RecipeDto>? Data { get; set; }
}
