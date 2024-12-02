using Zenject;
using UnityEngine;
using TDGame.BaseSpace;
using TDGame.GO;
namespace TDGame.Installers
{
    public class BaseInstaller : MonoInstaller
    {
        [SerializeField] private Base _base;
        [SerializeField] private SpawnPoint _spawnPoint;

        public override void InstallBindings()
        {
            Container.Bind<IBase>()
                .FromComponentInNewPrefab(_base)
                .AsSingle()
                .OnInstantiated((context, spawnerInstance) =>
                {
                    var fort = spawnerInstance as Base;
                    fort?.transform.SetParent(_spawnPoint.transform);
                })
                .NonLazy();
        }
    }
}

