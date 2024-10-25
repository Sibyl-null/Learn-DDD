namespace DomeGym.Domain.UnitTest.TestConstants;

public static partial class Constants
{
    public static class Room
    {
        public static readonly Guid Id = Guid.NewGuid();
        
        public const int MaxSessionCount = 10;
    }
}