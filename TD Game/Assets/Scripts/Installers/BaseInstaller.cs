using Zenject;
using UnityEngine;
using TDGame.Fabrics;

namespace TDGame.Installers
{
    public class BaseInstaller : MonoInstaller
    {
        [Inject] private IBaseFactory _factory;
        [SerializeField] private Base _base;
        [SerializeField] private MovePoint _movePoint;

        public override void InstallBindings()
        {
            var Base = _factory.Create(_base, _movePoint.transform.position, Quaternion.identity) as Base;

            Container.Bind<Base>().FromInstance(Base).AsSingle().NonLazy();
            Base.transform.SetParent(_movePoint.transform);
        }
    }
}

