using TDGame.BaseSpace.HealthSystem;
using TDGame.BaseSpace.SO;
using TDGame.Factories;

namespace TDGame.BaseSpace
{
    public interface IBase : IFactoryObject
    {
        BaseData Data { get; }
        IHealthComponent HealthComponent { get; }

    }
}

