using TDGame.Factories;
using TDGame.SO.Units;
using TDGame.UnitEntity.HealthSystem;
using UnityEngine;

namespace TDGame.UnitEntity
{
    public interface IUnit : IFactoryObject, ITransformable
    {
        Transform Position { get; }
        IUnitHealthComponent HealthComponent { get; }
        UnitData Data { get; }
    }
}

