
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public async Task Add(User user)
    {
        await Task.CompletedTask;
        _users.Add(user);
    }

    public async Task<User?> FindByEmail(string email)
    {
        await Task.CompletedTask;
        return _users.Find(user => user.Email == email);
    }

    public async Task<User?> FindById(UserId id)
    {
        await Task.CompletedTask;
        return _users.Find(user => user.Id == id);
    }

    public async Task Update(User user)
    {
        await Task.CompletedTask;
        _users.Remove(user);
        _users.Add(user);
    }
}
