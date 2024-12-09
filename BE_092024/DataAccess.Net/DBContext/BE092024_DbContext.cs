using DataAccess.Net.DataObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Net.DBContext;

public class BE092024_DbContext: DbContext
{
    public BE092024_DbContext(DbContextOptions options):base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasKey(x => x.OrderId);
        modelBuilder.Entity<OrderDetail>().HasKey(x => x.OrderDetailId);
        modelBuilder.Entity<Room>().HasKey(k => k.Id);
        modelBuilder.Entity<Product>().HasKey(k => k.ProductId);
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
    }
    
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<Room> Room { get; set; }
    public virtual DbSet<Order> Order { get; set; }
    public virtual DbSet<OrderDetail> OrderDetail { get; set; }
    public virtual DbSet<User> User { get; set; }
}