using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zip.Domain.Entities;

namespace Zip.Infrastructure.Data;

public class AppDbContext :  DbContext{

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    protected readonly IConfiguration _config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _config.GetConnectionString("UserConnectionString") ?? throw new InvalidOperationException("Missing connection string");

        _ = optionsBuilder.UseNpgsql(connectionString);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Account> Accounts { get; set; }
}
