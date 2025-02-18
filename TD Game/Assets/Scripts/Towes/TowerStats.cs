using System.Collections;
using System.Collections.Generic;
using TDGame.Towers.SO;
using UnityEngine;

namespace TDGame.Towers
{
    public class TowerStats : MonoBehaviour
    {
    [SerializeField] private TowerScriptableData _towerData; // SO с базовыми характеристиками

    public float Damage { get; private set; }
    public float FireRate { get; private set; }
    public float FireRadius { get; private set; }
    public float RotationSpeed { get; private set; }

    private void Awake()
    {
        if (_towerData == null)
        {
            Debug.LogError("TowerScriptableData не назначен!");
            return;
        }

        Damage = _towerData.Damage;
        FireRate = _towerData.FireRate;
        FireRadius = _towerData.FireRadius;
        RotationSpeed = _towerData.RotationSpeed;
    }

    public void IncreaseDamage(float amount)
    {
        Damage += amount;
        Debug.Log($"Урон увеличен на {amount}. Новый урон: {Damage}");
    }

    public void IncreaseFireRate(float amount)
    {
        FireRate += amount;
        Debug.Log($"Скорость стрельбы увеличена на {amount}. Новая скорость: {FireRate}");
    }

    public void IncreaseFireRadius(float amount)
    {
        FireRadius += amount;
        Debug.Log($"Дальность стрельбы увеличена на {amount}. Новая дальность: {FireRadius}");
    }

    public void IncreaseRotationSpeed(float amount)
    {
        RotationSpeed += amount;
        Debug.Log($"Скорость поворота увеличена на {amount}. Новая скорость: {RotationSpeed}");
    }
    }
}

