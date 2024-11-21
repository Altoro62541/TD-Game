using UnityEngine;
using Zenject;
using TDGame.EnemySpawnerSpace;
namespace TDGame.Installers
{    
    public class EnemySpawnInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private MovePoint _movePoint;

         public override void InstallBindings()
        {
            Container.Bind<IEnemySpawner>()
            .FromComponentInNewPrefab(_spawner)  // Создаём объект из префаба
            .AsSingle()  // Он будет создан только один раз
            .OnInstantiated((context, spawnerInstance) =>
            {
                var spawner = spawnerInstance as EnemySpawner;
                spawner?.transform.SetParent(_movePoint.transform);
            })
            .NonLazy();
        }
    }
}

