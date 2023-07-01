using CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;
using CleanArchitecture.Application.Common.Messages;
using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Departments;
using CleanArchitecture.Domain.Departments.ValueObjects;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Queries.GetDepartmentById;

internal sealed class GetDepartmentByIdQueryHandler : IQueryHandler<GetDepartmentByIdQuery, ErrorOr<Department>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<Department>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var departmentId = DepartmentId.Create(request.DepartmentId);

        if (await _departmentRepository.FindByIdAsync(departmentId, cancellationToken) is not Department department)
        {
            return Errors.Department.NotFound(departmentId);
        }

        return department;
    }
}