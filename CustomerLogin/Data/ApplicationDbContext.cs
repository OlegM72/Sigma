using CustomerLogin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CustomerLogin.Data
{
    // This is a separate DBContext to handle Identity database tables
    // Uses all the built-in Identity types
    // Adds roles
    // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?source=recommendations&view=aspnetcore-6.0
    // To use `Guid` as the key type -> / public class IdentityDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public IdentityDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

    /* To add a custom User type:
    public class ApplicationUser : IdentityUser<Guid> {
        public string CustomTag { get; set; }
    }

    // Use the built-in Identity types except with a custom User type
    public class IdentityDbContext<ApplicationUser>
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
    } 
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    And change IdentityUser to ApplicationUser   
    services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    */

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}