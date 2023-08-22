
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? FindByEmail(string email)
    {
        return _users.Find(user => user.Email == email);
    }

    public User? FindById(UserId id)
    {
        return _users.Find(user => user.Id == id);
    }

    public void Update(User user)
    {
        _users.Remove(user);
        _users.Add(user);
    }
}
