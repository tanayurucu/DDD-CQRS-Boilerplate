using CleanArchitecture.Application.Common.Messages;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Commands.DeleteDepartment;

public sealed record DeleteDepartmentCommand(Guid DepartmentId) : ICommand<ErrorOr<Deleted>>;