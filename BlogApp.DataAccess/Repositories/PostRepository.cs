using BlogApp.DataAccess.Context;
using BlogApp.DataAccess.Repositories.RepositoryInterfaces;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogAppDbContext _context;

    public PostRepository(BlogAppDbContext context)
    {
        _context = context;
    }

    public DbSet<Post> Posts => _context.Posts;

    public async Task AddPostAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public Task<Post?> GetPostByIdAsync(int id)
        => _context.Posts
        .Include(x => x.Commentaries)
        .ThenInclude(x => x.Author)
        .FirstOrDefaultAsync(x => x.Id == id);
}
