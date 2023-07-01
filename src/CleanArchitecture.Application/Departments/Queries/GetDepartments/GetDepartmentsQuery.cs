using CleanArchitecture.Application.Common.Messages;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Departments;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Queries.GetDepartments;

public record GetDepartmentsQuery
    (string SearchString, int PageSize, int PageNumber) : IQuery<ErrorOr<Paged<Department>>>;