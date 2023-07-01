using CleanArchitecture.Application.Common.Messages;
using CleanArchitecture.Domain.Departments;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Queries.GetDepartmentById;

public sealed record GetDepartmentByIdQuery(Guid DepartmentId) : IQuery<ErrorOr<Department>>;