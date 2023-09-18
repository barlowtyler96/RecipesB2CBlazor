using System.ComponentModel.DataAnnotations;

namespace RecipesB2CBlazor.Models;

public class RecipeModel    
{
    public int RecipeId { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(2000)]
    public string? Instructions { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
