using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    Task<List<User>> ListUsersByEmail(string? email);
    Task<List<User>> ListUsersByName(string? name);
    Task<User?> FindByEmail(string email);
    Task<User?> FindById(UserId id);
    Task Update(User user);
    Task Add(User user);
    Task<List<User>> FindUserFriendsById(UserId userId);
}
