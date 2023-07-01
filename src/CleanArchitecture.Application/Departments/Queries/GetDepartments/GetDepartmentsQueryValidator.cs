using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Domain.Common.Errors;

using FluentValidation;

namespace CleanArchitecture.Application.Departments.Queries.GetDepartments;

public sealed class GetDepartmentsQueryValidator : AbstractValidator<GetDepartmentsQuery>
{
    public GetDepartmentsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithError(Errors.Common.Pagination.InvalidPageNumber);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithError(Errors.Common.Pagination.InvalidPageSize);
    }
}