using DataAccess.Net.DataObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Net.DBContext;

public class BE092024_DbContext: DbContext
{
    public BE092024_DbContext(DbContextOptions options):base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasKey(k => k.Id);
        modelBuilder.Entity<Product>().HasKey(k => k.ProductId);
    }
    
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<Room> Room { get; set; }
}