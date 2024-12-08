using System;
using UniRx;

namespace TDGame.UnitEntity.HealthSystem
{
    public interface IUnitHealthComponent
    {
        event Action OnDead;
        event Action<object> OnHit;
        IReadOnlyReactiveProperty<float> Health { get; }
        IReadOnlyReactiveProperty<float> MaxHealth { get; }

        public void Damage (float damage, object damager = null);

    }

}

