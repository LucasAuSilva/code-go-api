
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.LevelAggregateRoot;

namespace CodeGo.Infrastructure.Persistance;

public class LevelRepository : ILevelRepository
{
    private static readonly List<Level> _levels = new();

    public void Add(Level level)
    {
        _levels.Add(level);
    }

    public Level? FindWhenPreviousIsNull()
    {
        return _levels.Find(level => level.PreviousLevel is null);
    }
}
