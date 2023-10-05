using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManager.DataAccess.Model;

namespace UserManager.DataAccess
{
    public class UserManagerDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserManagerDbContext(DbContextOptions<UserManagerDbContext> options) 
            : base(options)
        {
           // Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);

        }
    }
}