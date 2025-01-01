using System.ComponentModel.DataAnnotations;

namespace RecipesB2CBlazor.Models;

public class Recipe    
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string Description { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Instructions { get; set; }

    public string ImageUrl { get; set; }

    [Required]
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public bool IsFavorited { get; set; } = false;
}
