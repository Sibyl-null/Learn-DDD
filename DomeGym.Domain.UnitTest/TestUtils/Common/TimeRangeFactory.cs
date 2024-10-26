using DomeGym.Domain.Common.ValueObjects;
using Throw;

namespace DomeGym.Domain.UnitTest.TestUtils.Common;

public static class TimeRangeFactory
{
    public static TimeRange CreateFromHours(int startHour, int endHour)
    {
        startHour.Throw()
            .IfGreaterThanOrEqualTo(endHour)
            .IfNegative()
            .IfGreaterThan(23);
        
        endHour.Throw()
            .IfLessThan(1)
            .IfGreaterThan(24);

        return TimeRange.FromTimeOnly(
            start: TimeOnly.MinValue.AddHours(startHour),
            end: TimeOnly.MinValue.AddHours(endHour)).Value;
    }
}