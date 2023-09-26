using BookstoreAPI.Models;
using BookstoreAPI.Seed;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Data;
public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) :
        base(options)
    {
        _configuration = configuration;
    }

    public required DbSet<User> Users { get; set; }
    public required DbSet<BookGenre> BookGenres { get; set; }
    public required DbSet<Role> Roles { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(e => e.ResetToken);
        modelBuilder.Entity<BookGenre>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<Role>().HasIndex(e => e.Name).IsUnique();
        
        modelBuilder.Entity<BookGenre>().HasData(SeedBookGenre.Genres);
        modelBuilder.Entity<Role>().HasData(SeedRole.Roles);

        base.OnModelCreating(modelBuilder);
    }
}