using Microsoft.AspNetCore.Rewrite;
using Microsoft.Identity.Web;
using RecipesB2CBlazor.StartupConfig;

var builder = WebApplication.CreateBuilder(args);

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRewriter(
    new RewriteOptions().Add(
        context =>
        {
            if (context.HttpContext.Request.Path == "/MicrosoftIdentity/Account/SignedOut")
            {
                context.HttpContext.Response.Redirect("/");
            }
        }));

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
