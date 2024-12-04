using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sayHello.Entities;

namespace sayHello.DataAccess.Data.Config;

public class BlockedUserConfiguration : IEntityTypeConfiguration<BlockedUser>
{
    public void Configure(EntityTypeBuilder<BlockedUser> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.User)
            .WithMany(u => u.BlockedUsers) 
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Blocked_User)
            .WithMany(u => u.BlockedByUsers) 
            .HasForeignKey(b => b.BlockedUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(a => new { a.UserId, a.Blocked_User }) 
            .IsUnique()
            .HasDatabaseName("IX_BlockedUser_User_BlockedUser");

    }
}