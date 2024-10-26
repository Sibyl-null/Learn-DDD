using DomeGym.Domain.Common.ValueObjects;

namespace DomeGym.Domain.UnitTest.TestConstants;

public static partial class Constants
{
    public static class Session
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.Now);
        public static readonly TimeRange Time = TimeRange.FromTimeOnly(
            TimeOnly.MinValue.AddHours(8),
            TimeOnly.MinValue.AddHours(9)).Value;
        
        public const int MaxParticipantCount = 10;
    }
}