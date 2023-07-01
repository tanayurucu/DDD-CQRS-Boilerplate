using MediatR;

namespace CleanArchitecture.Domain.Common.Events;

public interface IDomainEvent : INotification
{
    public Guid Id { get; }
}