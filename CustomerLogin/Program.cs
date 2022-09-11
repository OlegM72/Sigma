using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using CustomerLogin.Data;
using CustomerLogin.Models;
using CustomerLogin.Services;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(); // recreate the structure of internal pages based on the project files - after scaffolding

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
// builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    //.AddRoles<IdentityRole>()
    //.AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // this is only for development (proposes to perform migrations)

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

/* https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/?view=aspnetcore-6.0&tabs=visual-studio
// External Authentication providers setup - needs the App name, URL and registration at the social accounts
builder.Services.AddAuthentication()
    .AddFacebook(options => {
        IConfigurationSection FBAuthNSection =
        config.GetSection("Authentication:FB");
        options.ClientId = FBAuthNSection["ClientId"];
        options.ClientSecret = FBAuthNSection["ClientSecret"];
    })
    .AddGoogle(options => {
        IConfigurationSection googleAuthNSection =
        config.GetSection("Authentication:Google");
        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
    })
    .AddTwitter(twitterOptions => {
        twitterOptions.ConsumerKey = config["Authentication:Twitter:ConsumerAPIKey"];
        twitterOptions.ConsumerSecret = config["Authentication:Twitter:ConsumerSecret"];
        twitterOptions.RetrieveUserDetails = true;
    });
*/

// example of Razor Identity Pages setup
// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings 
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

    // options.SignIn.RequireConfirmedAccount = true; // require confirmed account for sign in
    options.SignIn.RequireConfirmedEmail = true; // require email validation for sign in
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.Name = "_GymOffice";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);

    options.LoginPath = "/Identity/Account/Login";
    // options.LogoutPath = "/Identity/Account/Logout";
    // options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.AccessDeniedPath = "/Home/Denied";
    options.SlidingExpiration = true;
});

// If we will do so, only registered Customers will be able to login and use the App
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .RequireRole("Customer")
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // enabling Identity pages (adds authentication middleware to the request pipeline)
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
