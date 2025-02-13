using TDGame.GO.SO;
using TDGame.UnitEntity.HealthSystem;
using UnityEngine;

namespace TDGame.GO
{
    public class Projectile : MonoBehaviour
    {
    [SerializeField] private ProjectileData _data;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Transform _target;
    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        if (_target == null) return;

        Vector2 direction = ((Vector2)_target.position - (Vector2)_cachedTransform.position).normalized;
        _rigidbody.velocity = direction * _data.ProjectileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rigidbody.MoveRotation(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_data.AreaRadius > 0f)
        {

            Vector2 explosionCenter = collision.contacts.Length > 0 
                ? collision.contacts[0].point 
                : (Vector2)_cachedTransform.position;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionCenter, _data.AreaRadius);
            foreach (Collider2D col in colliders)
            {
                if (col.TryGetComponent(out UnitHealthComponent healthComponent))
                {
                    Debug.Log($"Explosion hit: Damage {_data.ProjectileDamage} applied.");
                    healthComponent.Damage(_data.ProjectileDamage, this);
                }
            }
        }
        else
        {
            if (collision.collider.TryGetComponent(out UnitHealthComponent healthComponent))
            {
                Debug.Log($"Direct hit! Damage: {_data.ProjectileDamage}");
                healthComponent.Damage(_data.ProjectileDamage, this);
            }
        }

        Destroy(gameObject);
    }
}
}


