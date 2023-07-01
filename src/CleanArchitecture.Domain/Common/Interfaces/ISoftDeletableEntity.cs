namespace CleanArchitecture.Domain.Common.Interfaces;

public interface ISoftDeletableEntity
{
    DateTime? DeletedOnUtc { get; }
    bool IsDeleted { get; }
}