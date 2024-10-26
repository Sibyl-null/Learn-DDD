using DomeGym.Domain.TrainerAggregate;
using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Trainers;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(Guid? id = null, Guid? userId = null)
    {
        return new Trainer(
            id: id ?? Constants.Trainer.Id,
            userId: userId ?? Constants.User.Id);
    }
}