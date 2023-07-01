using CleanArchitecture.Domain.Departments;
using CleanArchitecture.Domain.Departments.ValueObjects;

using FluentAssertions;

namespace CleanArchitecture.Domain.Tests.Departments;

public class DepartmentTests
{
    [Fact]
    public void Department_Should_BeCreatedWithValidDepartmentName()
    {
        // Arrange
        var departmentNameResult = DepartmentName.Create("TRIAL");

        // Act
        var department = Department.Create(departmentNameResult.Value);

        // Assert
        department.Name.Should().Be(departmentNameResult.Value);
    }

    [Fact]
    public void Department_Should_UpdateDepartmentName()
    {
        // Arrange
        var departmentName = DepartmentName.Create("TRIAL.1");
        var newDepartmentName = DepartmentName.Create("TRIAL.2");
        var department = Department.Create(departmentName.Value);

        // Act
        department.SetName(newDepartmentName.Value);

        // Assert
        department.Name.Should().Be(newDepartmentName.Value);
    }
}