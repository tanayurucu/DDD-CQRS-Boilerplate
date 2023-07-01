using CleanArchitecture.Application.Departments.Commands.CreateDepartment;
using CleanArchitecture.Application.Departments.Commands.DeleteDepartment;
using CleanArchitecture.Application.Departments.Commands.UpdateDepartment;
using CleanArchitecture.Application.Departments.Queries.GetDepartmentById;
using CleanArchitecture.Application.Departments.Queries.GetDepartments;
using CleanArchitecture.Contracts.Common;
using CleanArchitecture.Contracts.Departments;
using CleanArchitecture.Contracts.Routes;
using CleanArchitecture.Infrastructure.Authentication.Constants;
using CleanArchitecture.Presentation.Controllers.Base;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers.V1;

/// <summary>
/// Departments Controller
/// </summary>
[ApiVersion("1.0")]
public class DepartmentsController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    /// <summary>
    /// Creates Departments Controller
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="mapper"></param>
    public DepartmentsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }


    /// <summary>
    /// Gets paginated list of departments
    /// </summary>
    /// <param name="queryParameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(ApiRoutes.Departments.Get)]
    [Authorize(Policy = PolicyNames.RequireExpertPolicy)]
    [ProducesResponseType(typeof(PagedResponse<DepartmentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetDepartmentsQueryParameters queryParameters,
        CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetDepartmentsQuery>(queryParameters);

        var result = await _sender.Send(query, cancellationToken);

        return result.Match(departments => Ok(_mapper.Map<PagedResponse<DepartmentResponse>>(departments)),
            Problem);
    }


    /// <summary>
    /// Gets a department by ID
    /// </summary>
    /// <param name="departmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(ApiRoutes.Departments.GetById)]
    [ProducesResponseType(typeof(DepartmentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid departmentId, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetDepartmentByIdQuery>(departmentId);

        var result = await _sender.Send(query, cancellationToken);

        return result.Match(department => Ok(_mapper.Map<DepartmentResponse>(department)), Problem);
    }

    /// <summary>
    /// Creates a department
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost(ApiRoutes.Departments.Create)]
    [ProducesResponseType(typeof(DepartmentResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateDepartmentRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateDepartmentCommand>(request);

        var result = await _sender.Send(command, cancellationToken);

        return result.Match(department =>
                CreatedAtAction(
                    actionName: nameof(GetById),
                    routeValues: new { DepartmentId = department.Id.Value },
                    value: _mapper.Map<DepartmentResponse>(department)),
            Problem);
    }

    /// <summary>
    /// Updates a department
    /// </summary>
    /// <param name="departmentId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut(ApiRoutes.Departments.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(Guid departmentId, UpdateDepartmentRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateDepartmentCommand>((departmentId, request));

        var result = await _sender.Send(command, cancellationToken);

        return result.Match(_ => NoContent(), Problem);
    }

    /// <summary>
    /// Deletes a department
    /// </summary>
    /// <param name="departmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete(ApiRoutes.Departments.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid departmentId, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<DeleteDepartmentCommand>(departmentId);

        var result = await _sender.Send(command, cancellationToken);

        return result.Match(_ => NoContent(), Problem);
    }
}