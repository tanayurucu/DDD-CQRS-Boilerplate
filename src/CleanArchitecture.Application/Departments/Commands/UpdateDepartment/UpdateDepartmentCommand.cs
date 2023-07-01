using CleanArchitecture.Application.Common.Messages;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Commands.UpdateDepartment;

public sealed record UpdateDepartmentCommand(Guid DepartmentId, string Name) : ICommand<ErrorOr<Updated>>;