using TDGame.HealthSystem;
using UnityEngine;

namespace TDGame.Handlers
{
    public class EnabledComponentsHandler : MonoBehaviour
    {
        private IEnabledComponents[] _enabledComponents;
        private IHealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _enabledComponents = GetComponents<IEnabledComponents>();
        }

        private void OnDead()
        {
            foreach (var component in _enabledComponents)
            {
                component.Enabled = false;
            }
        }

        private void OnEnable()
        {
            _healthComponent.OnDead += OnDead;
        }

        private void OnDisable()
        {
            _healthComponent.OnDead -= OnDead;
        }
    }
}

