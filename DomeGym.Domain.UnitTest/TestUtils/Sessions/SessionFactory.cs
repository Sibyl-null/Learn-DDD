using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        Guid? id = null,
        Guid? trainerId = null,
        int maxParticipantCount = Constants.Session.MaxParticipantCount,
        DateOnly? date = null,
        TimeRange? time = null)
    {
        return new Session(
            id: id ?? Constants.Session.Id,
            trainerId: trainerId ?? Constants.Trainer.Id,
            maxParticipantCount: maxParticipantCount,
            date: date ?? Constants.Session.Date,
            time: time ?? Constants.Session.Time);
    }
}