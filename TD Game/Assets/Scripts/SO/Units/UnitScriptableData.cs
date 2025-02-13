using UnityEngine;

namespace TDGame.SO.Units
{
    [CreateAssetMenu(menuName = "Unit/New ScriptableData")]
    public class UnitScriptableData : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _speedMove;
        [SerializeField] private int _rewardAmount;

        public float Health => _health;
        public float Damage => _damage;
        public float SpeedMove => _speedMove;
        public int RewardAmount => _rewardAmount;
    }
}

