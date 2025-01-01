using RecipesB2CBlazor.Models;

namespace RecipesB2CBlazor.Helpers;

public class PaginationResponse<T> where T : class
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; } = 8;

    public int CurrentPageNumber { get; set; } = 1;

    public List<T>? Data { get; set; }
}
