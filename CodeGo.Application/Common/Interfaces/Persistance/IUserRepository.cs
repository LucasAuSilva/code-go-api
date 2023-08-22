
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    User? FindByEmail(string email);
    User? FindById(UserId id);
    void Update(User user);
    void Add(User user);
}
