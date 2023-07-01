using CleanArchitecture.Application.Common.Messages;
using CleanArchitecture.Domain.Departments;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Commands.CreateDepartment;

public sealed record CreateDepartmentCommand(string Name): ICommand<ErrorOr<Department>>;