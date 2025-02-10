using TDGame.Towers.Attack;
using UnityEngine;

namespace TDGame.Towers
{
    public class RocketTower : TowerBase
    {
        [SerializeField] private Transform _turretRotationPoint;
        

        protected override void RotateTowardsTarget()
        {
            float angle = Mathf.Atan2(_target.position.y - transform.position.y, 
            _target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.RotateTowards(_turretRotationPoint.rotation, targetRotation, _towerData.RotationSpeed * Time.deltaTime);
        }
    }
}

