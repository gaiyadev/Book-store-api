using BookstoreAPI.Models;
using BookstoreAPI.Seed;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Data;
public class ApplicationDbContext : DbContext
{
   private readonly string _databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new InvalidOperationException();
   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public required DbSet<User> Users { get; set; }
    public required DbSet<BookGenre> BookGenres { get; set; }
    public required DbSet<Role> Roles { get; set; }
    public required DbSet<Product> Products { get; set; }
    public required DbSet<CartItem> CartItems { get; set; }

    public required DbSet<OrderItem> OrderItems { get; set; }
    
    public required DbSet<Order> Orders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseUrl);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(e => e.ResetToken);
        
        modelBuilder.Entity<BookGenre>().HasIndex(e => e.Name).IsUnique();
        
        modelBuilder.Entity<Role>().HasIndex(e => e.Name).IsUnique();
        
        modelBuilder.Entity<Product>().HasIndex(e => e.ProductSlug).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(e => e.UserId);
        modelBuilder.Entity<Product>().HasIndex(e => e.BookGenreId);
        modelBuilder.Entity<Product>().HasIndex(e => e.Price);

        modelBuilder.Entity<CartItem>().HasIndex(e => e.UserId);
        modelBuilder.Entity<CartItem>().HasIndex(e => e.ProductId);

        modelBuilder.Entity<Order>().HasIndex(e => e.UserId);
        modelBuilder.Entity<Order>().HasIndex(e => e.TrackingId);
        
        // modelBuilder.Entity<Order>()
        //     .HasMany(o => o.OrderItems)
        //     .WithOne(oi => oi.Order)
        //     .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>().HasIndex(e => e.ProductId);

        
        modelBuilder.Entity<BookGenre>().HasData(SeedBookGenre.Genres);
        modelBuilder.Entity<Role>().HasData(SeedRole.Roles);
        

        base.OnModelCreating(modelBuilder);
    }
}