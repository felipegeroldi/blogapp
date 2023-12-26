using BlogApp.Models;

namespace BlogApp.DataAccess.Repositories.RepositoryInterfaces;

public interface IUserRepository
{
    public IQueryable<User> Users { get; }
    public Task<User?> GetUserByIdAsync(int id);
    public Task<User?> GetUserByEmailAsync(string email);
    public Task AddUserAsync(User user);
}
