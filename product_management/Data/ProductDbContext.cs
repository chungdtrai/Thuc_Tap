using Microsoft.EntityFrameworkCore;
using product_management.Models;

namespace product_management.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
