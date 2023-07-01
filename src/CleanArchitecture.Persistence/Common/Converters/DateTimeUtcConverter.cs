using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanArchitecture.Persistence.Common.Converters;

public class DateTimeUtcConverter : ValueConverter<DateTime, DateTime>
{
    public DateTimeUtcConverter() : base(dateTime => dateTime,
        dateTime => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc))
    {
    }
}