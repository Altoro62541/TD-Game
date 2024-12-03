using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using TDGame.Factories.Units;
using TDGame.GO;
using TDGame.SO.Spawn;
using TDGame.UI;
using UnityEngine;
using Zenject;
namespace TDGame.Spawn
{
    public class UnitSpawner : MonoBehaviour
    {
    [Inject] private IUnitFactoryGO _factory;
    [Inject] private WaveUI _waveUI;
    [SerializeField] private UnitSpawnPoint _spawnPoint;
    [SerializeField] private UnitSpawnConfig _spawnConfig;

    public void Start()
    {
        StartWaves().Forget();
    }

    private async UniTask StartWaves()
    {
        for (int wave = 0; wave < _spawnConfig.TotalWaves; wave++) 
        {
            Debug.Log($"Запуск волны {wave + 1}");
            await Spawning(wave); 

            if (wave < _spawnConfig.TotalWaves - 1)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnConfig.WaveInterval));
            }
        }

        Debug.Log("Все волны завершены!");
    }

    private async UniTask Spawning(int wave)
    {

    var SortedUnits = _spawnConfig.Elements
        .OrderByDescending(e => e.UnitsCount)
        .SelectMany(e => Enumerable.Repeat(e.Prefab, e.UnitsCount))
        .ToList();

    int UnitsToSpawn = _spawnConfig.GetUnitsPerWave(wave); 
    TimeSpan SpawnInterval = _spawnConfig.GetSpawnInterval(wave);

    int SpawnedUnits = 0;
    foreach (var unit in SortedUnits)
    {
        if (SpawnedUnits >= UnitsToSpawn) break;

        Vector3 Position = _spawnPoint.transform.position;
        if (Position != Vector3.zero)
        {
            _factory.Create(unit, Position, Quaternion.identity, null);
            _waveUI.UpdateWave(wave + 1, _spawnConfig.TotalWaves);
            SpawnedUnits++;
        }

        await UniTask.Delay(SpawnInterval);
    }
    }
    }
}

