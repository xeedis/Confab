using Confab.Modules.Users.Core.DAL;
using Confab.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Users.Core.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(UsersDbContext context)
    {
        _context = context;
        _users = _context.Users;
    }
    
    public async Task<User> GetAsync(Guid id)
        => await _users.SingleOrDefaultAsync(u => u.Id == id);

    public async Task<User> GetAsync(string email)
        => await _users.SingleOrDefaultAsync(u => u.Email == email);

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _users.Update(user);
        await _context.SaveChangesAsync();
    }
}