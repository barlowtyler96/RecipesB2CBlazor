﻿using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RecipesB2CBlazor.Services;
namespace RecipesB2CBlazor.StartupConfig;

public static class DependencyInjectionExtensions
{
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
        builder.Services.AddRecipesService();
        builder.Services.AddUsersService();
    }

    public static void AddAuthServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"))
            .EnableTokenAcquisitionToCallDownstreamApi(new string[] { builder.Configuration["DownstreamApi:Scopes"] })
            .AddInMemoryTokenCaches();

        builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
    }
}
