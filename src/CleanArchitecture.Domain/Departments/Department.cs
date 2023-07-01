using CleanArchitecture.Domain.Common.Abstractions;
using CleanArchitecture.Domain.Common.Interfaces;
using CleanArchitecture.Domain.Departments.ValueObjects;

namespace CleanArchitecture.Domain.Departments;

public sealed class Department : AggregateRoot<DepartmentId>, IAuditableEntity, ISoftDeletableEntity
{
    public DepartmentName Name { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public DateTime? DeletedOnUtc { get; private set; }

    public bool IsDeleted { get; private set; }

    private Department(DepartmentId id, DepartmentName name)
        : base(id)
    {
        Name = name;
    }

    private Department() { }

    public static Department Create(DepartmentName name) => new(DepartmentId.CreateUnique(), name);
    public void SetName(DepartmentName name) => Name = name;
}