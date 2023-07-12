using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RecipesB2CBlazor.Services;

namespace RecipesB2CBlazor.StartupConfig;

public static class DependencyInjectionExtensions
{
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRecipesService(builder.Configuration);
    }

    public static void AddAuthServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAdB2C")
            .EnableTokenAcquisitionToCallDownstreamApi(new string[] { builder.Configuration["DownstreamApi:Scopes"] })
            .AddInMemoryTokenCaches();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        }).AddMicrosoftIdentityUI();

        builder.Services.Configure<OpenIdConnectOptions>(builder.Configuration.GetSection("AzureAdB2C"));
    }
}
