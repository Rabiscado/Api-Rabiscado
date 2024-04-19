using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Rabiscado.Infra.Converters;

public sealed class TimeOnlyCustomConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyCustomConverter() 
        : base(d => d.ToTimeSpan(), d => TimeOnly.FromTimeSpan(d))
    { }
}
