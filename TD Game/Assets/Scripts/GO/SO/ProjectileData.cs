using UnityEngine;

namespace TDGame.GO.SO
{
    [CreateAssetMenu(menuName = "GO/Projectile")]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _projectileDamage;
        [SerializeField] private float _areaRadius; 

        public float ProjectileSpeed => _projectileSpeed;
        public float ProjectileDamage => _projectileDamage;
        public float AreaRadius => _areaRadius;
    }
}

