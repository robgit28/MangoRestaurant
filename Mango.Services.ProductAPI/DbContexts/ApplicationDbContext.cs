using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // creates our database in SSMS 
        public DbSet<Product> Products { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1, 
                Name = "Samosas", 
                Price = 3,
                Description = "Fried pastry with a savory filling of spiced potatoes, onions, and peas",
                ImageUrl = "https://robgit28.blob.core.windows.net/mango/samosas.jpeg",
                CategoryName = "Starter"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Onion Bhajis",
                Price = 3.50,
                Description = "Crispy Onion Bhajis with thinly sliced onions, mixed and coated in a chickpea flour batter",
                ImageUrl = "https://robgit28.blob.core.windows.net/mango/onionBhajis.jpeg",
                CategoryName = "Starter"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Paneer Pakora",
                Price = 2.50,
                Description = "Paneer pakoras fried, cheesy squares, served with green & red chili chutney",
                ImageUrl = "https://robgit28.blob.core.windows.net/mango/paneerPakora.jpeg",
                CategoryName = "Starter"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Aloo Gobi",
                Price = 4.50,
                Description = "Potato cauliflower vegetable curry",
                ImageUrl = "https://robgit28.blob.core.windows.net/mango/alooGobi.jpeg",
                CategoryName = "Starter"
            });
        }
    }
}
