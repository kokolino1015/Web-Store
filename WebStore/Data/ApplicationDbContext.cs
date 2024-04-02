using WebStore.Data.Entities.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Data.Entities;
using WebStore.Models.CategoryModel;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Tls;

namespace WebStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Sale> Sales { get; set; }



        // Seed Roles and Admin User 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string ADMIN_ID = Guid.NewGuid().ToString();
            string ROLE_ID = Guid.NewGuid().ToString();

            //Seed roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = ROLE_ID, ConcurrencyStamp = ROLE_ID },
                new IdentityRole { Name = "Employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = Guid.NewGuid().ToString()}
            );

            // Seed carts table 
            builder.Entity<Cart>().HasData(new Cart { Id = 1 });

            //create user
            var appUser = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "admin@webshop.com",
                EmailConfirmed = true,
                UserName = "admin@webshop.com",
                NormalizedUserName = "ADMIN@WEBSHOP.COM",
                NormalizedEmail = "ADMIN@WEBSHOP.COM", // Cannot login without it
                CartId = 1 // If missing an error is generated when start the application
            };
            //Cart Id ????

            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "%aDmInS741&");

            //seed user
            builder.Entity<ApplicationUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

    }
}