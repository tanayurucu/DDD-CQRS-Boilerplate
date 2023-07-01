namespace CleanArchitecture.Domain.Common.Interfaces;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; }
    DateTime? ModifiedOnUtc { get; }
}