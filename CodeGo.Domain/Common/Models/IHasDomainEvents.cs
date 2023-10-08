
namespace CodeGo.Domain.Common.Models;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public List<IDomainEvent> PopDomainEvents();
}
