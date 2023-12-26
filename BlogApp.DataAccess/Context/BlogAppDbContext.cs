using BlogApp.DataAccess.Mappings;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Context;

public class BlogAppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Commentary> Commentaries { get; set; }
    public DbSet<Post> Posts { get; set; }

    public BlogAppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new CommentaryMap());
        modelBuilder.ApplyConfiguration(new PostMap());
    }
}
