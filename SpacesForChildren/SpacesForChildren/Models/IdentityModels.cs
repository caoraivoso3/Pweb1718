using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SpacesForChildren.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int FiscalId { get; set; }
        public string Address { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Line below added
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Child { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Local> Local { get; set; }
        public DbSet<RequestInfo> RequestInfo { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Service> Service { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Institution>().ToTable("Institution");
            modelBuilder.Entity<Parent>().ToTable("Parent");
            base.OnModelCreating(modelBuilder);
        }
    }
}