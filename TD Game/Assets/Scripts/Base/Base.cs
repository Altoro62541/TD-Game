using TDGame.BaseSpace.HealthSystem;
using TDGame.BaseSpace.SO;
using TDGame.Handlers;
using UnityEngine;

namespace TDGame.BaseSpace
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(BaseEnabledComponentsHandler))]

    public class Base : MonoBehaviour, IBase
    {
        [SerializeField] private BaseData _baseData;
        private HealthComponent _healthComponent;

        public IHealthComponent HealthComponent => _healthComponent;
        public Vector3 Position => transform.position;
        public Transform Transform => transform;
        public BaseData Data => _baseData;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.SetMaxHealth(_baseData.Health);
            _healthComponent.SetHealth(_baseData.Health);
        }

    }
}