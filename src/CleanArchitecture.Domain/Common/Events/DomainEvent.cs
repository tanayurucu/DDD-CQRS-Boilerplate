namespace CleanArchitecture.Domain.Common.Events;

public abstract record DomainEvent(Guid Id) : IDomainEvent;