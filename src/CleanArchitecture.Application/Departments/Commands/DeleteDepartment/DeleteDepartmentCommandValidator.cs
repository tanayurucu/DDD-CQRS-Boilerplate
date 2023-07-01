using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Domain.Common.Errors;

using FluentValidation;

namespace CleanArchitecture.Application.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithError(Errors.Department.Id.Empty)
            .NotEqual(Guid.Empty)
            .WithError(Errors.Department.Id.Empty);
    }
}