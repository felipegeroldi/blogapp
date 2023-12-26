using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mappings;

internal class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.Property(x => x.Content)
            .HasColumnType("NTEXT");

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Posts)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
