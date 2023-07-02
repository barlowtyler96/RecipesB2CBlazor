using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;


var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDistributedMemoryCache();

builder.Services.Configure<CookiePolicyOptions>(opts =>
{
    opts.CheckConsentNeeded = context => true;
    opts.MinimumSameSitePolicy = SameSiteMode.Unspecified;
    opts.HandleSameSiteCookieCompatibility();
});

builder.Services.AddOptions();

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi(new string[] { builder.Configuration["DownstreamApi:Scopes"] })
.AddInMemoryTokenCaches();

builder.Services.AddRecipesService(builder.Configuration);






builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

//builder.Services.AddHttpClient("api", opts =>
//{
//    opts.BaseAddress = new Uri(builder.Configuration.GetValue<string>("DownstreamApi:BaseUrl"));
//});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
