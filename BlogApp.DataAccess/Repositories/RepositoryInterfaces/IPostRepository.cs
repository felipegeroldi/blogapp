using BlogApp.Models;

namespace BlogApp.DataAccess.Repositories.RepositoryInterfaces;

public interface IPostRepository
{
    public Task AddPostAsync(Post post);
    public Task<Post?> GetPostByIdAsync(int id);
}
