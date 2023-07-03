using RecipesB2CBlazor.Services;
using System.Runtime.CompilerServices;

namespace RecipesB2CBlazor.StartupConfig;

public static class DependencyInjectionExtensions
{
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRecipesService(builder.Configuration);
    }

    public static void AddAuthServices(this WebApplicationBuilder builder)
    {
        
    }
}
