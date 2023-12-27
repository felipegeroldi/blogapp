using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Repositories.RepositoryInterfaces;

public interface IPostRepository
{
    public Task AddPostAsync(Post post);
    public Task<Post?> GetPostByIdAsync(int id);
    public DbSet<Post> Posts { get; }
}
