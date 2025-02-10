using TDGame.Towers.Attack;
using UnityEngine;

namespace TDGame.Towers
{
    public class ProjectileTower : TowerBase
    {
        [SerializeField] private Transform _turretRotationPoint;

    protected override void RotateTowardsTarget()
    {
        if (_target == null) return;
        Vector2 direction = _target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        _turretRotationPoint.rotation = Quaternion.RotateTowards(_turretRotationPoint.rotation, targetRotation, _towerData.RotationSpeed * Time.deltaTime);
    }
    }
}

