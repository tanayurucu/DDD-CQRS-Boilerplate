using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;
using CleanArchitecture.Application.Common.Messages;
using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Departments;
using CleanArchitecture.Domain.Departments.ValueObjects;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Commands.DeleteDepartment;

public sealed class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommand, ErrorOr<Deleted>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var departmentId = DepartmentId.Create(request.DepartmentId);
        if (await _departmentRepository.FindByIdAsync(departmentId, cancellationToken) is not Department department)
        {
            return Errors.Department.NotFound(departmentId);
        }

        _departmentRepository.Remove(department);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}