
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CodeGoDbContext _dbContext;

    public UserRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> FindByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email); 
    }

    public async Task<User?> FindById(UserId id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id); 
    }

    public async Task<List<User>> ListUsersByEmail(string? email)
    {
        if (email is null)
            return await _dbContext.Users.ToListAsync();
        return await _dbContext.Users
            .Where(user => user.Email.Contains(email))
            .ToListAsync();
    }

    public async Task Update(User user)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
