using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Departments.ValueObjects;

using FluentAssertions;

namespace CleanArchitecture.Domain.Tests.Departments;

public class DepartmentNameTests
{
    [Fact]
    public void DepartmentName_Should_Not_BeCreatedWithEmptyString()
    {
        // Arrange
        string departmentNameString = string.Empty;

        // Act
        var departmentName = DepartmentName.Create(departmentNameString);

        // Assert
        departmentName.IsError.Should().BeTrue();
        departmentName.FirstError.Should().Be(Errors.Department.Name.Empty);
    }

    [Fact]
    public void DepartmentName_Should_Not_BeCreatedWithWhitespace()
    {
        // Arrange
        const string departmentNameString = " ";

        // Act
        var departmentName = DepartmentName.Create(departmentNameString);

        // Assert
        departmentName.IsError.Should().BeTrue();
        departmentName.FirstError.Should().Be(Errors.Department.Name.Empty);
    }

    [Fact]
    public void DepartmentName_Should_Not_BeCreatedWithTooLongString()
    {
        // Arrange
        var departmentNameString = "DepartmentNameThatIsTooLong";

        // Act
        var departmentName = DepartmentName.Create(departmentNameString);

        // Assert
        departmentName.IsError.Should().BeTrue();
        departmentName.FirstError.Should().Be(Errors.Department.Name.TooLong);
    }

    [Fact]
    public void DepartmentName_Should_BeCreatedWithValidString()
    {
        // Arrange
        var departmentNameString = "TRIAL";

        // Act
        var departmentName = DepartmentName.Create(departmentNameString);

        // Assert
        departmentName.IsError.Should().BeFalse();
        departmentName.Value.Value.Should().Be(departmentNameString);
    }
}