using TDGame;
using TDGame.Fabrics;
using UnityEngine;
using Zenject;

public class BaseFactory : IBaseFactory
{
    [Inject] private DiContainer _container;
    public IBase Create(Base prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        return _container.InstantiatePrefabForComponent<Base>(prefab, position, rotation, parent);
    }
}
