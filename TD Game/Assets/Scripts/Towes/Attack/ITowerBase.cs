using UnityEngine;
namespace TDGame.Towers.Attack
{
    public interface ITowerBase
    {
        void Attack(Transform target);
        Transform FindTarget();
    }
}

