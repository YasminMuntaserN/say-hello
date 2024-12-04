using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sayHello.Entities;

namespace sayHello.DataAccess.Data.Config;

public class BlockedUserConfiguration : IEntityTypeConfiguration<BlockedUser>
{
    public void Configure(EntityTypeBuilder<BlockedUser> builder)
    {
        builder.HasKey(b => b.BlockedUserId);

        builder.Property(e => e.DateBlocked)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.Property(e => e.Reason)
            .HasMaxLength(200);

        builder.HasOne(b => b.User)
            .WithMany(u => u.BlockedUsers)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.BlockedByUser)
            .WithMany(u => u.BlockedByUsers)
            .HasForeignKey(b => b.BlockedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => new { a.UserId, a.BlockedByUserId })
            .IsUnique()
            .HasDatabaseName("IX_BlockedUser_User_BlockingUser");
    }
}