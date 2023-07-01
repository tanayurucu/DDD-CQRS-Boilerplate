namespace CleanArchitecture.Contracts.Departments;

public record DepartmentResponse(Guid Id, string Name, DateTime CreatedOnUtc, DateTime? ModifiedOnUtc);