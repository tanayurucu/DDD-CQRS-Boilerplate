using CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Departments;
using CleanArchitecture.Domain.Departments.ValueObjects;
using CleanArchitecture.Persistence.Common.Abstractions;
using CleanArchitecture.Persistence.Common.Extensions;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

internal sealed class DepartmentRepository : AsyncRepository<Department, DepartmentId>, IDepartmentRepository
{
    public DepartmentRepository(CleanArchitectureDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Department?> FindByNameAsync(DepartmentName name, CancellationToken cancellationToken = default)
    {
        return (from department in _dbContext.Set<Department>()
            where department.Name == name
            select department).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Paged<Department>> FindManyWithPaginationAsync(string searchString, int pageSize, int pageNumber,
        CancellationToken cancellationToken = default)
    {
        return (from department in _dbContext.Set<Department>()
            where string.IsNullOrEmpty(searchString) || ((string)department.Name).Contains(searchString)
            orderby department.Name
            select department).ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }
}