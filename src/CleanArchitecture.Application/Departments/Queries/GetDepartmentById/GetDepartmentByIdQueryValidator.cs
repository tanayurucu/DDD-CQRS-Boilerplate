using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Domain.Common.Errors;

using FluentValidation;

namespace CleanArchitecture.Application.Departments.Queries.GetDepartmentById;

public sealed class GetDepartmentByIdQueryValidator : AbstractValidator<GetDepartmentByIdQuery>
{
    public GetDepartmentByIdQueryValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithError(Errors.Department.Id.Empty)
            .NotEqual(Guid.Empty)
            .WithError(Errors.Department.Id.Empty);
    }
}