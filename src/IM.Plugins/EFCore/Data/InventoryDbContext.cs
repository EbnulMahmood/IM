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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.NewGuid(), Name = "Electronics", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Fruits", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Groceries", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Beverage", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Cosmetics", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Est ullamcorper", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Nisi est", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Semper feugiat", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Commodo quis", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Metus vulputate", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Faucibus pulvinar", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Massa sed", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Posuere lorem", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Auctor urna", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Id = Guid.NewGuid(), Name = "Eget nunc", CreatedBy = Guid.Empty, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." }
            );
        }
    }
}