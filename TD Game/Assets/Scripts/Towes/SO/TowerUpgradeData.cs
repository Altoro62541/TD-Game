using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDGame.Towers.SO
{
    [CreateAssetMenu(fileName = "TowerUpgradeData", menuName = "Tower/Upgrade Data", order = 0)]
    public class TowerUpgradeData : ScriptableObject
    {
        [Header("Параметры улучшения")]
        public int baseUpgradeCost = 100;
        public float upgradeMultiplier = 1.5f;
        public int maxUpgradeLevel = 3;


        [Header("Статистика башни")]
        public float damageIncrease = 10f;
        public float rangeIncrease = 1f;
        public float fireRateIncrease = 0.2f;
        public float rotationSpeedIncrease = 0.5f;
    }
}

