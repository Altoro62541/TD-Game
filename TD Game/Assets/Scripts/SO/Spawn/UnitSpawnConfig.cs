using System;
using System.Collections.Generic;
using TDGame.UnitEntity.SO;
using UnityEngine;
namespace TDGame.SO.Spawn
{
    [CreateAssetMenu(menuName = "Units/UnitSpawnerConfig")]
    public class UnitSpawnConfig : ScriptableConfig
    {
        [SerializeField] private UnitSpawnConfigElement[] _elements;
        public  IEnumerable<UnitSpawnConfigElement> Elements => _elements;

        public int TotalWaves = 5; 
        public int BaseUnitsPerWave = 3;
        public int WaveInterval = 5;
        public float SpawnInterval = 2;

        public int GetUnitsPerWave(int wave)
        {
            return BaseUnitsPerWave + wave;
        }

        public TimeSpan GetSpawnInterval(int wave)
        {
            return TimeSpan.FromSeconds(SpawnInterval);
        }
    }
}

