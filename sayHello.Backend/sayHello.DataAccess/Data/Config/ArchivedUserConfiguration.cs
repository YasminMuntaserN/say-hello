using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sayHello.Entities;

namespace sayHello.DataAccess.Data.Config;

public class ArchivedUserConfiguration : IEntityTypeConfiguration<ArchivedUser>
{
    public void Configure(EntityTypeBuilder<ArchivedUser> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.User)
            .WithMany(u => u.ArchivedUsers) 
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Archived_User)
            .WithMany(u => u.ArchivedByUsers) 
            .HasForeignKey(b => b.ArchivedUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Unique constraint to prevent archiving the same user multiple times
        builder.HasIndex(a => new { a.UserId, a.ArchivedUserId }) 
            .IsUnique()
            .HasDatabaseName("IX_ArchivedUser_User_ArchivedUser");
    }
}