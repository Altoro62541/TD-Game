using System;
using TDGame.Money;
using TDGame.SO.Units;
using UniRx;
using UnityEngine;

namespace TDGame.UnitEntity.HealthSystem
{

    public class UnitHealthComponent : MonoBehaviour, IUnitHealthComponent
    {
    [SerializeField] private UnitScriptableData _data;
    [SerializeField] private FloatReactiveProperty _health = new FloatReactiveProperty();
    [SerializeField] private FloatReactiveProperty _maxHealth = new FloatReactiveProperty();

    public event Action<object> OnHit;
    public event Action OnDead;

    public IReadOnlyReactiveProperty<float> Health => _health;
    public IReadOnlyReactiveProperty<float> MaxHealth => _maxHealth;

    private void Awake()
    {
        if (_data == null)
        {
            Debug.LogError("UnitScriptableData is not assigned!");
            return;
        }

        _maxHealth.Value = _data.Health;
        _health.Value = _data.Health;

        OnDead += HandleUnitDeath;

        Debug.Log($"Initialized Health: {_health.Value}, MaxHealth: {_maxHealth.Value}");
    }

    public void Damage(float damage, object damager = null)
    {
        if (damage <= 0f)
        {
            throw new ArgumentException("Damage must be greater than 0");
        }

        Debug.Log($"Damage called! Damage: {damage}, Current Health: {_health.Value}");
        OnHit?.Invoke(damager);

        _health.Value = Mathf.Clamp(_health.Value - damage, 0, _maxHealth.Value);
        Debug.Log($"New Health: {_health.Value}");

        if (_health.Value <= 0f)
        {
            Debug.Log("Unit is dead!");
            OnDead?.Invoke();
        }
    }

    public void SetHealth(float health)
    {
        _health.Value = Mathf.Clamp(health, 0, _maxHealth.Value);
    }

    public void SetMaxHealth(float maxHealthValue)
    {
        _maxHealth.Value = Mathf.Clamp(maxHealthValue, 1, float.MaxValue);
        _health.Value = Mathf.Clamp(_health.Value, 0, _maxHealth.Value);
    }

    private void OnValidate()
    {
        _maxHealth.Value = Mathf.Max(_maxHealth.Value, 1);
        _health.Value = Mathf.Clamp(_health.Value, 0, _maxHealth.Value);
    }

    private void HandleUnitDeath()
    {
        Destroy(gameObject);
        Currency.Instance.AddCurrency(_data.RewardAmount);
        OnDead -= HandleUnitDeath;
    }
    }
}
