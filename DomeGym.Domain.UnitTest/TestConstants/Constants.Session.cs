namespace DomeGym.Domain.UnitTest.TestConstants;

public static partial class Constants
{
    public static class Session
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.Now);
        public static readonly TimeOnly StartTime = TimeOnly.MinValue.AddHours(8);
        public static readonly TimeOnly EndTime = TimeOnly.MinValue.AddHours(9);
        
        public const int MaxParticipants = 10;
    }
}