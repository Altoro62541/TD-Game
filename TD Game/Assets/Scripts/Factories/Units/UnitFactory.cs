using TDGame.UnitEntity;
using UnityEngine;
using Zenject;
namespace TDGame.Factories.Units
{
    public class UnitFactoryGO :  IUnitFactoryGO
    {
        [Inject] private DiContainer _container;
        public IUnit Create(Unit prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return  _container.InstantiatePrefabForComponent<IUnit>(prefab, position, Quaternion.identity, null);
        }
    }
}

