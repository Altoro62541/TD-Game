using System;
using UniRx;

namespace TDGame.BaseSpace.HealthSystem
{
    public interface IHealthComponent
    {
        event Action OnDead;
        event Action<object> OnHit;
        IReadOnlyReactiveProperty<float> Health { get; }
        IReadOnlyReactiveProperty<float> MaxHealth { get; }

        public void Damage (float damage, object damager = null);

    }
}

