﻿using System.ComponentModel.DataAnnotations;

namespace RecipesB2CBlazor.Models;

public class RecipeIngredient
{
    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public string? Unit { get; set; }

    [Required]
    public string? IngredientName { get; set; }
}
