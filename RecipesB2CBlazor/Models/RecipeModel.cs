﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipesB2CBlazor.Models;

public class RecipeModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(2000)]
    public string? Ingredients { get; set; }

    [Required]
    [MaxLength(1000)]
    public string? Instructions { get; set; }

    [MaxLength(200)]
    public string? ImagePath { get; set; }
}

