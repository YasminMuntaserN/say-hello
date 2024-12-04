using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sayHello.Entities;

namespace sayHello.DataAccess;

public class AppDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<BlockedUser> BlockedUsers { get; set; }
    public DbSet<ArchivedUser> ArchivedUsers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Media> Medias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("Connection");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}