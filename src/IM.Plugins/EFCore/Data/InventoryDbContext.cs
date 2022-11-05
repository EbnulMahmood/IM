using IM.CoreBusiness.Entities;
using Microsoft.EntityFrameworkCore;

namespace IM.Plugins.EFCore.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 2, Name = "Fruits", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 3, Name = "Groceries", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 4, Name = "Beverage", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 5, Name = "Cosmetics", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 6, Name = "Est ullamcorper", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 7, Name = "Nisi est", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 8, Name = "Semper feugiat", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 9, Name = "Commodo quis", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 10, Name = "Metus vulputate", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 11, Name = "Faucibus pulvinar", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 12, Name = "Massa sed", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 13, Name = "Posuere lorem", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 14, Name = "Auctor urna", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = 15, Name = "Eget nunc", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product {Id = 1,  Name = "Laptop", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 54000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 1},
                new Product {Id = 2,  Name = "Desktop", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 500, PricePerUnit = 14000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 1},
                new Product {Id = 3,  Name = "Samsung Mobile", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 20, PricePerUnit = 54030, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 1},
                new Product {Id = 4,  Name = "Velit euismod", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 52000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 2},
                new Product {Id = 5,  Name = "Nibh praesent", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 54020, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 2},
                new Product {Id = 6,  Name = "Nunc aliquet", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 4000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 2},
                new Product {Id = 7,  Name = "Metus aliquam", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 24000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 2},
                new Product {Id = 8,  Name = "A iaculis", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 24000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 2},
                new Product {Id = 9,  Name = "Arcu vitae", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 5000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 1},
                new Product {Id = 10,  Name = "Orci a scelerisque", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 43000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 1},
                new Product {Id = 11,  Name = "Dolor magna", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Limited = false, InStock = 200, PricePerUnit = 51000, BasicUnit = "piece", CreatedAt = DateTime.Now, CategoryId = 1}
            );
        }
    }
}