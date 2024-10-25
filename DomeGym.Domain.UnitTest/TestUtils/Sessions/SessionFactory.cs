using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        Guid? id = null,
        Guid? trainerId = null,
        int maxParticipants = Constants.Session.MaxParticipants,
        DateOnly? date = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null)
    {
        return new Session(
            id: id ?? Constants.Session.Id,
            trainerId: trainerId ?? Constants.Trainer.Id,
            maxParticipants: maxParticipants,
            date: date ?? Constants.Session.Date,
            startTime: startTime ?? Constants.Session.StartTime,
            endTime: endTime ?? Constants.Session.EndTime);
    }
}