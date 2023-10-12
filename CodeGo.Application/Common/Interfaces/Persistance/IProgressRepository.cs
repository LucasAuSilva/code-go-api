
using CodeGo.Domain.ProgressAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IProgressRepository
{
    Task AddAsync(Progress progress);
}
