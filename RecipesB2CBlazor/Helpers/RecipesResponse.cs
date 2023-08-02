using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Helpers;

public class RecipesResponse
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; } = 6;

    public int CurrentPageNumber { get; set; } = 1;

    public List<RecipeModel> Data { get; set; }
}
