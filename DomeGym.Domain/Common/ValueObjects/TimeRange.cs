using ErrorOr;
using Throw;

namespace DomeGym.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public static ErrorOr<TimeRange> FromDateTimes(DateTime start, DateTime end)
    {
        if (start.Date != end.Date)
            return Error.Validation(description: "Start and end times must be on the same date.");
        
        if (start >= end)
            return Error.Validation(description: "Start time must be before end time.");
        
        return new TimeRange(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end));
    }

    public static ErrorOr<TimeRange> FromTimeOnly(TimeOnly start, TimeOnly end)
    {
        if (start >= end)
            return Error.Validation(description: "Start time must be before end time.");
        
        return new TimeRange(start, end);
    }
    
    public TimeOnly Start { get; }
    public TimeOnly End { get; }

    private TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public bool OverlapsWith(TimeRange other)
    {
        return Start < other.End && End > other.Start;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}