using CleanArchitecture.Domain.Departments;
using CleanArchitecture.Domain.Departments.ValueObjects;
using CleanArchitecture.Persistence.Common.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(TableNames.Departments);

        builder.HasKey(department => department.Id);

        builder.Property(department => department.Id).ValueGeneratedNever()
            .HasConversion(id => id.Value, value => DepartmentId.Create(value));

        builder.Property(department => department.Name)
            .HasConversion(name => name.Value, value => DepartmentName.Create(value).Value)
            .HasMaxLength(DepartmentName.MaxLength);

        builder.Property(department => department.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(department => !department.IsDeleted);
    }
}