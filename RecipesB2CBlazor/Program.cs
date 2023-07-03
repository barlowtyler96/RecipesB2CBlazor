using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RecipesB2CBlazor.Services;
using RecipesB2CBlazor.StartupConfig;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDistributedMemoryCache();

//builder.Services.Configure<CookiePolicyOptions>(opts =>
//{
//    opts.CheckConsentNeeded = context => true;
//    opts.MinimumSameSitePolicy = SameSiteMode.Unspecified;
//    opts.HandleSameSiteCookieCompatibility();
//});

builder.AddCustomServices();
builder.AddAuthServices();

builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
