using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvoiceR.Infrastructure.Config.Converters;

internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
        d => d.ToDateTime(TimeOnly.MinValue),
        d => DateOnly.FromDateTime(d))
    { }
}