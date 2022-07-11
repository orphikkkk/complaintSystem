using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SajhaSabal;

public class SsdbContext : IdentityDbContext
{
    public SsdbContext(DbContextOptions<SsdbContext> options) : base(options)
    {

    }

    // public DbSet<CategoryModel> Categories { get; set; }
    // public DbSet<ProductModel> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<CategoryModel>().ToTable("Category");
        // builder.Entity<ProductModel>().ToTable("Product");

        base.OnModelCreating(builder);
    }
}