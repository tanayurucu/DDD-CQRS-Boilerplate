using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;
using CleanArchitecture.Application.Common.Messages;
using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Departments;
using CleanArchitecture.Domain.Departments.ValueObjects;

using ErrorOr;

namespace CleanArchitecture.Application.Departments.Commands.CreateDepartment;

public sealed class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, ErrorOr<Department>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Department>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        ErrorOr<DepartmentName> createDepartmentNameResult = DepartmentName.Create(request.Name);
        if (createDepartmentNameResult.IsError)
        {
            return createDepartmentNameResult.Errors;
        }

        if (await _departmentRepository.FindByNameAsync(createDepartmentNameResult.Value,
                cancellationToken) is not null)
        {
            return Errors.Department.AlreadyExists(createDepartmentNameResult.Value);
        }

        var department = Department.Create(createDepartmentNameResult.Value);
        await _departmentRepository.AddAsync(department, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return department;
    }
}