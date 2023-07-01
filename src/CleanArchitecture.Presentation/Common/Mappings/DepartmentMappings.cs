using CleanArchitecture.Application.Departments.Commands.CreateDepartment;
using CleanArchitecture.Application.Departments.Commands.DeleteDepartment;
using CleanArchitecture.Application.Departments.Commands.UpdateDepartment;
using CleanArchitecture.Application.Departments.Queries.GetDepartmentById;
using CleanArchitecture.Application.Departments.Queries.GetDepartments;
using CleanArchitecture.Contracts.Departments;
using CleanArchitecture.Domain.Departments;
using CleanArchitecture.Domain.Departments.ValueObjects;

using Mapster;

namespace CleanArchitecture.Presentation.Common.Mappings;

/// <summary>
/// Mappings for Department
/// </summary>
public sealed class DepartmentMappings : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Department, DepartmentResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name.Value);

        config.NewConfig<GetDepartmentsQueryParameters, GetDepartmentsQuery>();

        config.NewConfig<CreateDepartmentRequest, CreateDepartmentCommand>();

        config.NewConfig<Guid, GetDepartmentByIdQuery>().Map(dest => dest.DepartmentId, src => src);

        config.NewConfig<Guid, DeleteDepartmentCommand>().Map(dest => dest.DepartmentId, src => src);

        config
            .NewConfig<(Guid DepartmentId, UpdateDepartmentRequest request), UpdateDepartmentCommand>()
            .Map(dest => dest.Name, src => src.request.Name)
            .Map(dest => dest.DepartmentId, src => src.DepartmentId);
    }
}