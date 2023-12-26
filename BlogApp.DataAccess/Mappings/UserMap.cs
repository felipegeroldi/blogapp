using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mappings;

internal class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnType("NVARCHAR(80)")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnType("NVARCHAR(120)")
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasColumnType("NVARCHAR(100)")
            .HasColumnName("PasswordHash")
            .IsRequired();

        builder.Property(x => x.UserRole)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.DeletedAt);

        builder.HasIndex(x => x.Email);
    }
}
