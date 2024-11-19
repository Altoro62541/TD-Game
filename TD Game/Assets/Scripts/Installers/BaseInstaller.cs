using Zenject;
using UnityEngine;

namespace TDGame.Installers
{
    public class BaseInstaller : MonoInstaller
    {
        [SerializeField] private Base _base;
        [SerializeField] private MovePoint _movePoint;

        public override void InstallBindings()
        {
            Container.Bind<IEnemySpawner>()
                .FromComponentInNewPrefab(_base)
                .AsSingle()
                .OnInstantiated((context, spawnerInstance) =>
                {
                    var fort = spawnerInstance as EnemySpawner;
                    fort?.transform.SetParent(_movePoint.transform);
                })
                .NonLazy();
        }
    }
}

