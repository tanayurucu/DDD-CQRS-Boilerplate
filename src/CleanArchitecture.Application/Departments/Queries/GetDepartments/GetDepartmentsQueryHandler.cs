using CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;
using CleanArchitecture.Application.Common.Messages;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Departments;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Queries.GetDepartments;

internal sealed class GetDepartmentsQueryHandler : IQueryHandler<GetDepartmentsQuery, ErrorOr<Paged<Department>>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<Paged<Department>>> Handle(GetDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _departmentRepository.FindManyWithPaginationAsync(request.SearchString, request.PageSize,
            request.PageNumber, cancellationToken);
    }
}