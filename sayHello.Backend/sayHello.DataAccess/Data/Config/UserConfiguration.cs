using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sayHello.Entities;

namespace sayHello.DataAccess.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId);

        builder.HasIndex(e => e.Email).IsUnique().HasDatabaseName("IX_Users_Email");

        builder.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(50); 

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(e => e.ProfilePictureUrl)
            .HasMaxLength(200);

        builder.Property(e => e.Bio)
            .IsRequired(false)
            .HasMaxLength(500); 

        builder.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(20) 
            .HasDefaultValue("Offline");

        builder.Property(e => e.DateJoined)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); 

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(e => e.LastLogin)
            .HasColumnType("datetime");

        builder.HasOne(e => e.AuthUser)
            .WithOne(e => e.User)
            .HasForeignKey<AuthUser>(au => au.UserId);;
    }
}