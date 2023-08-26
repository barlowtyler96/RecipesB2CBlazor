using System.ComponentModel.DataAnnotations;

namespace RecipesB2CBlazor.Models;

public class RecipeDto
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Instructions { get; set; }

    public string? ImagePath { get; set; }

    [Required]
    public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
