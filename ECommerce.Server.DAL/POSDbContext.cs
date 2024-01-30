using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ECommerce.Server.Models.DataModels;

namespace ECommerce.Server.DAL
{
    public class POSDbContext(DbContextOptions<POSDbContext> options) : IdentityDbContext<POSUser>(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
