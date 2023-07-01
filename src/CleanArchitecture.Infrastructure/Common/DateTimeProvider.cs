using CleanArchitecture.Application.Common.Interfaces.Infrastructure;

namespace CleanArchitecture.Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.Now;

    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
}