using Throw;

namespace DomeGym.Domain;

public readonly struct TimeRange
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public bool OverlapsWith(TimeRange other)
    {
        return Start < other.End && End > other.Start;
    }
}