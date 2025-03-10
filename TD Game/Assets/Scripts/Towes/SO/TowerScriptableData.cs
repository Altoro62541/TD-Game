using TDGame.GO;
using UnityEngine;

namespace TDGame.Towers.SO
{
    [CreateAssetMenu(menuName = "Tower/TowerData")]
    public class TowerScriptableData : ScriptableObject
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _fireRadius;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private int _price;

        public float Damage => _damage;
        public float FireRate => _fireRate;
        public float FireRadius => _fireRadius;
        public float RotationSpeed => _rotationSpeed;
        public GameObject ProjectilePrefab => _projectilePrefab;
        public int Price => _price;

    }
}

    
