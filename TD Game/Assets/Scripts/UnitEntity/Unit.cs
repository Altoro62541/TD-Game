using System;
using TDGame.Handlers;
using TDGame.SO.Units;
using TDGame.UnitEntity.HealthSystem;
using UnityEngine;
namespace TDGame.UnitEntity
{
    [RequireComponent(typeof(UnitHealthComponent))]
    [RequireComponent(typeof(EnabledComponentsHandler))]
    public class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] private UnitData _data;
        [SerializeField] private UnitScriptableData _scriptableData;
        private UnitHealthComponent _healthComponent;
        public UnitData Data => _data;
        public Transform Transform => transform;
        public IUnitHealthComponent HealthComponent => _healthComponent;

        private void Awake()
        {
            if (_scriptableData is null)
            {
                throw new NullReferenceException(nameof(_data));
            }
                _data = new(_scriptableData);
            
            _healthComponent = GetComponent<UnitHealthComponent>();
            
        }
    }
}

