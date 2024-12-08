
using TDGame.BaseSpace;
using TDGame.SO.Units;
using UnityEngine;
namespace TDGame.UnitEntity
{
    public class UnitDamageHandler : MonoBehaviour
    {
        
        [SerializeField] private UnitScriptableData _data;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out IBase fort))
            {
                fort.HealthComponent.Damage(_data.Damage);
                Destroy(gameObject);
            }
        }

    }
}

