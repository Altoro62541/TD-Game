using System.Collections;
using System.Collections.Generic;
using TDGame.Money;
using TDGame.Towers.SO;
using UnityEngine;
using Zenject;

namespace TDGame.Towers
{
    public class TowerUpgrade : MonoBehaviour
    {
        [SerializeField] private TowerUpgradeData _upgradeData; // SO с настройками улучшения

    private int currentUpgradeLevel = 0;
    private int currentUpgradeCost;
    private Currency _currency; // Зависимость внедряется через Zenject

    [Inject]
    public void Construct(Currency currency)
    {
        _currency = currency;
    }

    private void Awake()
    {
        if (_upgradeData == null)
        {
            Debug.LogError("TowerUpgradeData не назначен!");
            return;
        }
        currentUpgradeCost = _upgradeData.baseUpgradeCost;
    }

    /// <summary>
    /// Попытка улучшить башню: проверяется достаточность валюты и максимальный уровень.
    /// </summary>
    public void TryUpgrade()
{
    if (_currency == null)
    {
        Debug.LogError("Зависимость _currency не внедрена!");
        return;
    }

    if (currentUpgradeLevel >= _upgradeData.maxUpgradeLevel)
    {
        Debug.Log("Башня достигла максимального уровня улучшения.");
        return;
    }

    if (_currency.CurrentMoney >= currentUpgradeCost)
    {
        _currency.AddCurrency(-currentUpgradeCost);
        Upgrade();
    }
    else
    {
        Debug.Log("Не хватает денег для улучшения башни.");
    }
}

    /// <summary>
    /// Применение улучшения: повышение статистики и обновление стоимости следующего улучшения.
    /// </summary>
    private void Upgrade()
    {
        currentUpgradeLevel++;
        Debug.Log($"Башня улучшена до уровня {currentUpgradeLevel}.");

        TowerStats stats = GetComponent<TowerStats>();
        if (stats != null)
        {
            stats.IncreaseDamage(_upgradeData.damageIncrease);
            stats.IncreaseFireRate(_upgradeData.fireRateIncrease);
            stats.IncreaseFireRadius(_upgradeData.rangeIncrease);
            stats.IncreaseRotationSpeed(_upgradeData.rotationSpeedIncrease);
        }

        currentUpgradeCost = Mathf.RoundToInt(currentUpgradeCost * _upgradeData.upgradeMultiplier);
    }
    }
}
