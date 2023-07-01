using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Departments.ValueObjects;

using FluentValidation;

namespace CleanArchitecture.Application.Departments.Commands.UpdateDepartment;

public sealed class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithError(Errors.Department.Id.Empty)
            .NotEqual(Guid.Empty)
            .WithError(Errors.Department.Id.Empty);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithError(Errors.Department.Name.Empty)
            .MaximumLength(DepartmentName.MaxLength)
            .WithError(Errors.Department.Name.TooLong);
    }
}