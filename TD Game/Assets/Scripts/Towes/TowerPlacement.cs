using System;
using Cysharp.Threading.Tasks;
using TDGame.GO;
using TDGame.Towers.Attack;
using UniRx;
using UnityEngine;
namespace TDGame.Towers
{
    public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private GameObject _rocketTowerPrefab;
    [SerializeField] private GameObject _projectileTowerPrefab;
    [SerializeField] private float _buildDelay = 1.5f;
    [SerializeField] private LayerMask _spawnPlaceLayer;

    private Camera _mainCamera;
    private bool _isBuilding;

    private readonly Subject<string> _towerBuilt = new Subject<string>();
    public IObservable<string> OnTowerBuilt => _towerBuilt;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_isBuilding) return;
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTowerAsync(_rocketTowerPrefab, "Ракетная башня").Forget();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TryPlaceTowerAsync(_projectileTowerPrefab, "Снарядная башня").Forget();
        }
    }

    private async UniTaskVoid TryPlaceTowerAsync(GameObject towerPrefab, string towerName)
    {
        _isBuilding = true;

        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, _spawnPlaceLayer);

        if (hit.collider != null && hit.collider.TryGetComponent(out TowerPlace place) && !place.IsOccupied.Value)
        {
            await BuildTower(place, towerPrefab, towerName);
        }
        else
        {
            Debug.Log("Место занято или недоступно для строительства!");
        }

        _isBuilding = false;
    }

    private async UniTask BuildTower(TowerPlace place, GameObject towerPrefab, string towerName)
    {
        place.SetOccupied(true);
        Debug.Log($"Строительство башни: {towerName}...");

        await UniTask.Delay(TimeSpan.FromSeconds(_buildDelay));

        GameObject tower = Instantiate(towerPrefab, place.transform.position, Quaternion.identity);
        tower.transform.SetParent(place.transform);

        Debug.Log($"Башня {towerName} успешно построена на позиции {place.transform.position}");
        _towerBuilt.OnNext(towerName);
    }
}
}



