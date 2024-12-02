using UnityEngine;

namespace TDGame.SO.Units
{
    public class UnitData
    {
        public float Health {get; set;}
        public float Damage {get; set;}
        public float SpeedMove  {get; set;}
        public UnitData(UnitScriptableData data)
        {
            Health = data.Health;
            Damage = data.Damage;
            SpeedMove = data.SpeedMove;
        }
        
    }
}

