using UnityEngine;

namespace TDGame.SO.Units
{
    [CreateAssetMenu(menuName = "Unit/New Data")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _speedMove;

        public float Health => _health;
        public float Damage => _damage;
        public float SpeedMove => _speedMove;
    }
}

