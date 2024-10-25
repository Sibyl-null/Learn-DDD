using ErrorOr;

namespace DomeGym.Domain;

public class Schedule
{
    public static Schedule Empty()
    {
        return new Schedule(Guid.NewGuid());
    }

    private readonly Guid _id;
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar;

    public Schedule(Guid? id, Dictionary<DateOnly, List<TimeRange>>? calendar = null)
    {
        _id = id ?? Guid.NewGuid();
        _calendar = calendar ?? new();
    }

    public ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange>? timeSlots))
        {
            _calendar[date] = [time];
            return Result.Success;
        }

        if (timeSlots.Any(tr => tr.OverlapsWith(time)))
            return Error.Conflict();
        
        timeSlots.Add(time);
        return Result.Success;
    }
}