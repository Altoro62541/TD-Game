using TDGame.GO;
using TDGame.UnitsSpawner;
using UnityEngine;
using Zenject;

namespace TDGame.Installers
{
    public class UnitSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private UnitSpawner _spawner;
        [SerializeField] private SpawnPoint _spawnPoint;

        public override void InstallBindings()
        {
            Container.Bind<IUnitSpawner>()
                .FromComponentInNewPrefab(_spawner)
                .AsSingle()
                .OnInstantiated((context, spawnerInstance) =>
                {
                    var spawner = spawnerInstance as UnitSpawner;
                    spawner?.transform.SetParent(_spawnPoint.transform);
                })
                .NonLazy();
        }
    }
}
