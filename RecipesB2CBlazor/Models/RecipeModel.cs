using System.ComponentModel.DataAnnotations;

namespace RecipesB2CBlazor.Models;

public class RecipeModel    
{
    public int RecipeId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Instructions { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
