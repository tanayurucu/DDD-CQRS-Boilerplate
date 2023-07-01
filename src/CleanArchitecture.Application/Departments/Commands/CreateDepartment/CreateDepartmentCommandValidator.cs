using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Departments.ValueObjects;

using FluentValidation;

namespace CleanArchitecture.Application.Departments.Commands.CreateDepartment;

public sealed class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithError(Errors.Department.Name.Empty)
            .MaximumLength(DepartmentName.MaxLength)
            .WithError(Errors.Department.Name.TooLong);
    }
}