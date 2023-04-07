using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Presistence;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Review> Reviews  => Set<Review>();
    public DbSet<User> Users  => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Review>()
            .HasKey(o => new { o.UserId, o.BookId });
        
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(32)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(u => u.AccountStatus)
            .HasConversion<string>()
            .IsRequired();
        
        
        modelBuilder.Entity<Review>()
            .Property(r => r.Rate)
            .HasPrecision(1, 0)
            .IsRequired();
        
        modelBuilder.Entity<Review>()
            .Property(r => r.Content)
            .HasMaxLength(1000);


        modelBuilder.Entity<Book>()
            .Property(b => b.Description)
            .HasMaxLength(1000);

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(250);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _configuration.GetConnectionString("DatabaseConnection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));

        options.UseMySql(connectionString, serverVersion)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
}