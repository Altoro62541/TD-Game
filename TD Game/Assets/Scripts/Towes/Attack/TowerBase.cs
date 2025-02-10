using TDGame.GO;
using TDGame.Towers.SO;
using UnityEditor;
using UnityEngine;

namespace TDGame.Towers.Attack
{
    public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] private TowerScriptableData _towerScriptableData;
    [SerializeField] protected LayerMask _enemyMask;
    [SerializeField] protected Transform _firePos;

    protected TowerData _towerData;
    protected Transform _target;
    protected float _cooldown;

    protected virtual void Awake()
    {
        _towerData = new TowerData(_towerScriptableData);
        Debug.Log("TowerBase Awake called");
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        if (_towerData == null) return;
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _towerData.FireRadius);
    }
#endif

    protected virtual void Update()
    {
        if (_target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!IsTargetInRange())
        {
            _target = null;
        }
        else
        {
            _cooldown += Time.deltaTime;
            if (_towerData.FireRate > 0f && _cooldown >= 1f / _towerData.FireRate)
            {
                Shoot();
                _cooldown = 0f;
            }
        }
    }

    protected virtual void Shoot()
    {
        GameObject projectileObject = Instantiate(_towerData.ProjectilePrefab, _firePos.position, Quaternion.identity);
        Projectile projectileScript = projectileObject.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(_target);
        }
    }

    protected virtual void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _towerData.FireRadius, _enemyMask);

        if (hits.Length > 0)
        {
            Transform closestTarget = null;
            float closestDistanceSqr = float.MaxValue;
            Vector2 currentPos = transform.position;
            foreach (var hit in hits)
            {
                float distSqr = ((Vector2)hit.transform.position - currentPos).sqrMagnitude;
                if (distSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distSqr;
                    closestTarget = hit.transform;
                }
            }
            _target = closestTarget;
            

           // _target = hits[0].transform;
        }
    }

    protected virtual bool IsTargetInRange()
    {
        if (_target == null) return false;
        float sqrDistance = ((Vector2)_target.position - (Vector2)transform.position).sqrMagnitude;
        return sqrDistance <= _towerData.FireRadius * _towerData.FireRadius;
    }

    protected virtual void RotateTowardsTarget()
    {
        if (_target == null) return;
        Vector2 direction = _target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _towerData.RotationSpeed * Time.deltaTime);
    }
}
}

