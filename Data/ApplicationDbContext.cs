using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeShopDSD605.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BikeShopDSD605.Models.Customers> Customers { get; set; } = default!;
        public DbSet<BikeShopDSD605.Models.Orders> Orders { get; set; } = default!;
        public DbSet<BikeShopDSD605.Models.Staffs> Staffs { get; set; } = default!;
        public DbSet<BikeShopDSD605.Models.Stocks> Stocks { get; set; } = default!;

        public DbSet<BikeShopDSD605.Models.Cast> Cast { get; set; } = default!;
        public DbSet<BikeShopDSD605.Models.Movie> Movie { get; set; } = default!;
    }
}