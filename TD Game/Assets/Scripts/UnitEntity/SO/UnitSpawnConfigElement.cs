using System;
using UnityEngine;
namespace TDGame.UnitEntity.SO
{
    [Serializable]
    public class UnitSpawnConfigElement
    {
        [SerializeField] private Unit _prefab;
        [SerializeField] private int _unitsCount;
        public Unit Prefab => _prefab;
        public int UnitsCount => _unitsCount;
    }
}

