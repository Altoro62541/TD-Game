using TDGame.GO;
using UnityEngine;

namespace TDGame.Towers.SO
{
    public class TowerData
    {
        public float Damage {get; set;}
        public float FireRate  {get; set;}
        public float FireRadius {get; set;}
        public float RotationSpeed {get; set;}
        public GameObject ProjectilePrefab {get; set;}
        public TowerData(TowerScriptableData data)
        {
            Damage = data.Damage;
            FireRate = data.FireRate;
            FireRadius = data.FireRadius;
            RotationSpeed = data.RotationSpeed;
            ProjectilePrefab = data.ProjectilePrefab;
        }
    }

}
