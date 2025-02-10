using UnityEngine;

namespace TDGame.GO.SO
{
    [CreateAssetMenu(menuName = "GO/Projectile")]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _projectileDamage;

        public float ProjectileSpeed => _projectileSpeed;
        public float ProjectileDamage => _projectileDamage;
    }
}

