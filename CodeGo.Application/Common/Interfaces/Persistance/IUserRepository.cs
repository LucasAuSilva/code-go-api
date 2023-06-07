
using CodeGo.Domain.UserAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    User? FindByEmail(string email);
    void Add(User user);
}
