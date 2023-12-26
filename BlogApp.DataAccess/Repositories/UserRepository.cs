using BlogApp.DataAccess.Context;
using BlogApp.DataAccess.Repositories.RepositoryInterfaces;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    public readonly BlogAppDbContext _context;

    public UserRepository(BlogAppDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetUserByEmailAsync(string email) =>
        _context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public Task<User?> GetUserByIdAsync(int id) =>
        _context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public IQueryable<User> Users =>
        _context.Users;

}
