using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using sayHello.DTOs.Message;
using sayHello.Entities;

namespace sayHello.DataAccess;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<AuthUser> AuthUsers { get; set; }
    public DbSet<BlockedUser> BlockedUsers { get; set; }
    public DbSet<ArchivedUser> ArchivedUsers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<ConversationDetailsDto> ConversationDetails { get; set; }
    public DbSet<UserConnection> UserConnections { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}