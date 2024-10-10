using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;

namespace pratice_10._10._24
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                var products = db.Products.Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 50 && p.UnitsInStock > 0).OrderBy(p => p.ProductName).ToList();

                Console.WriteLine("Products from $10 to $50, which are in warehouse: ");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.UnitPrice}, In Stock: {product.UnitsInStock}");
                }
            }
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=ProductDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Jivchik", UnitPrice = 15, UnitsInStock = 20 },
                new Product { ProductId = 2, ProductName = "PS3 Game", UnitPrice = 5, UnitsInStock = 0 },
                new Product { ProductId = 3, ProductName = "Sega Mega Drive", UnitPrice = 30, UnitsInStock = 10 },
                new Product { ProductId = 4, ProductName = "Nintendo Wii", UnitPrice = 50, UnitsInStock = 5 },
                new Product { ProductId = 5, ProductName = "TV", UnitPrice = 60, UnitsInStock = 15 }
            );
        }
    }
}
