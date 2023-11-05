using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Extensions;
using BasketballLeagueTracker.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();
services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("BasketCS")).ConfigureWarnings(warnings =>
            warnings.Ignore(CoreEventId.InvalidIncludePathError)));
// Application user and users roles
services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

var configuration = builder.Configuration;


// Facebook Auth
services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
    facebookOptions.Events = new OAuthEvents
    {
        OnRemoteFailure = (context) =>
        {
            
            context.HandleResponse(); 
            context.Response.Redirect("/"); 
            return Task.CompletedTask;
        }
    };
}).AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    googleOptions.Events = new OAuthEvents
    {
        OnRemoteFailure = (context) =>
        {

            context.HandleResponse();
            context.Response.Redirect("/");
            return Task.CompletedTask;
        }
    };
});

services.AddRazorPages(); // Added for login/reg

services.AddValidatorsFromAssemblyContaining<GameViewModel>()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();


services.AddRepositories(); // Repositories register 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Checking if username/password is valid
app.UseAuthorization();

app.MapRazorPages(); // Added for login/reg
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=League}/{action=Index}/{id?}");

app.Run();
