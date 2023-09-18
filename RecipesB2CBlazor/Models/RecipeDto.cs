namespace RecipesB2CBlazor.Models;

public class RecipeDto
{
    public int RecipeId { get; set; } 

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsFavorite { get; set; } = false;
}

