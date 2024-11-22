using System;
using TDGame.Handlers;
using TDGame.HealthSystem;
using TDGame.SO.Units;
using UnityEngine;
namespace TDGame.UnitEntity
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(EnabledComponentsHandler))]
    public class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] private UnitData _data;
        private HealthComponent _healthComponent;
        public UnitData Data => _data;
        public Transform Transform => transform;
        public IHealthComponent HealthComponent => _healthComponent;

        private void Awake()
        {
            if (_data is null)
            {
                throw new NullReferenceException(nameof(_data));
            }
            _healthComponent = GetComponent<HealthComponent>();
        }
    }
}

