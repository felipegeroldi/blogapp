using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mappings;

internal class CommentaryMap : IEntityTypeConfiguration<Commentary>
{
    public void Configure(EntityTypeBuilder<Commentary> builder)
    {
        builder.ToTable("commentaries");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Content)
            .HasColumnType("NTEXT")
            .IsRequired();

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Commentaries)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Post)
            .WithMany(x => x.Commentaries)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
