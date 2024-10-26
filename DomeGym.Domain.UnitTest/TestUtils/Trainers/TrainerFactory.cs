using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Trainers;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(Guid? id = null)
    {
        return new Trainer(id: id ?? Constants.Trainer.Id);
    }
}