using TDGame.Factories;
using TDGame.HealthSystem;
using TDGame.SO.Units;

namespace TDGame.UnitEntity
{
    public interface IUnit : IFactoryObject, ITransformable
    {
        IHealthComponent HealthComponent { get; }
        UnitData Data { get; }
    }
}

