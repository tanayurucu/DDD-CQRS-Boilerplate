using CleanArchitecture.Contracts.Common;

namespace CleanArchitecture.Contracts.Departments;

public class GetDepartmentsQueryParameters : PaginationQueryParameters
{
    public string SearchString { get; set; } = string.Empty;
}