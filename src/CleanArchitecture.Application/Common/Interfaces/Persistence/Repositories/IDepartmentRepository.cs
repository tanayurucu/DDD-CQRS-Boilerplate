using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Departments;
using CleanArchitecture.Domain.Departments.ValueObjects;

namespace CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;

public interface IDepartmentRepository : IAsyncRepository<Department, DepartmentId>
{
    Task<Department?> FindByNameAsync(DepartmentName name, CancellationToken cancellationToken = default);

    Task<Paged<Department>> FindManyWithPaginationAsync(string searchString, int pageSize, int pageNumber,
        CancellationToken cancellationToken = default);
}