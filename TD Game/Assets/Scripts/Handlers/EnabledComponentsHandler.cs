
using TDGame.UnitEntity.HealthSystem;
using UnityEngine;

namespace TDGame.Handlers
{
    public class EnabledComponentsHandler : MonoBehaviour
    {
        private IEnabledComponents[] _enabledComponents;
        private IUnitHealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<UnitHealthComponent>();
    if (_healthComponent == null)
    {
        Debug.LogError("UnitHealthComponent не найден на объекте: " + gameObject.name);
    }

    _enabledComponents = GetComponents<IEnabledComponents>();
    if (_enabledComponents.Length == 0)
    {
        Debug.LogWarning("Не найдено компонентов, реализующих IEnabledComponents на объекте: " + gameObject.name);
    }
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

