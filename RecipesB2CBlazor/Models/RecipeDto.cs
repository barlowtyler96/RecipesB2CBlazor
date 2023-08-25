namespace RecipesB2CBlazor.Models;

public class RecipeDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Instructions { get; set; }

    public string? ImagePath { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
