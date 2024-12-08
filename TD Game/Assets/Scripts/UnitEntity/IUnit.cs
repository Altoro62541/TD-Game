using TDGame.Factories;
using TDGame.SO.Units;
using TDGame.UnitEntity.HealthSystem;

namespace TDGame.UnitEntity
{
    public interface IUnit : IFactoryObject, ITransformable
    {
        IUnitHealthComponent HealthComponent { get; }
        UnitData Data { get; }
    }
}

