
using CodeGo.Domain.LevelAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface ILevelRepository
{
    Level? FindWhenPreviousIsNull();
    void Add(Level level);
}
