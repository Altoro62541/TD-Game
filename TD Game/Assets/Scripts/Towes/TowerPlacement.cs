using System;
using Cysharp.Threading.Tasks;
using TDGame.GO;
using TDGame.Money;
using TDGame.Towers.SO;
using UniRx;
using UnityEngine;
using Zenject;
namespace TDGame.Towers
{
    public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private GameObject _rocketTowerPrefab;
    [SerializeField] private GameObject _projectileTowerPrefab;
    [SerializeField] private float _buildDelay = 1.5f;
    [SerializeField] private LayerMask _spawnPlaceLayer;

    // Ссылки на данные для получения цены
    [SerializeField] private TowerScriptableData _rocketTowerData;
    [SerializeField] private TowerScriptableData _projectileTowerData;

    private Camera _mainCamera;
    private bool _isBuilding;
    private TowerPlace _selectedPlace;

    private DiContainer _container;
    private PurchasePanel _purchasePanel;
    private Currency _currency;

    // Внедрение зависимостей через Zenject
    [Inject]
    public void Construct(DiContainer container, PurchasePanel purchasePanel, Currency currency)
    {
        _container = container;
        _purchasePanel = purchasePanel;
        _currency = currency;
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        // Подписываемся на событие выбора башни из панели
        _purchasePanel.OnTowerSelectedEvent += BuildSelectedTower;
    }

    private void Update()
    {
        if (_isBuilding)
            return;

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, _spawnPlaceLayer);

            if (hit.collider != null &&
                hit.collider.TryGetComponent(out TowerPlace place) &&
                !place.IsOccupied.Value)
            {
                _selectedPlace = place;
                // Показываем панель выбора башни над выбранной точкой
                _purchasePanel.ShowPanel(place.transform.position);
            }
            else
            {
                Debug.Log("Место занято или недоступно для строительства!");
            }
        }
        else if (Input.GetMouseButtonDown(1) && _purchasePanel.IsActive)
        {
            _purchasePanel.HidePanel();
        }
    }

    // Вызывается, когда из панели приходит выбор башни (0 – Ракетная, 1 – Снарядная)
    public async void BuildSelectedTower(int towerType)
    {
        if (_selectedPlace == null)
            return;

        GameObject towerPrefab = null;
        string towerName = string.Empty;
        int towerPrice = 0;

        if (towerType == 0)
        {
            towerPrefab = _rocketTowerPrefab;
            towerName = "Ракетная башня";
            towerPrice = _rocketTowerData.Price;
        }
        else if (towerType == 1)
        {
            towerPrefab = _projectileTowerPrefab;
            towerName = "Снарядная башня";
            towerPrice = _projectileTowerData.Price;
        }
        else
        {
            Debug.LogWarning("Неизвестный тип башни!");
            return;
        }

        // Проверяем, достаточно ли денег
        if (_currency.CurrentMoney < towerPrice)
        {
            Debug.Log("Недостаточно денег для постройки башни!");
            _purchasePanel.HidePanel();
            return;
        }

        _isBuilding = true;
        _purchasePanel.HidePanel();

        // Списываем деньги
        _currency.AddCurrency(-towerPrice);

        await BuildTower(_selectedPlace, towerPrefab, towerName);

        _selectedPlace = null;
        _isBuilding = false;
    }

    private async UniTask BuildTower(TowerPlace place, GameObject towerPrefab, string towerName)
    {
        place.SetOccupied(true);
        Debug.Log($"Строительство башни: {towerName}...");

        await UniTask.Delay(System.TimeSpan.FromSeconds(_buildDelay));

        // Создаем башню через Zenject
        GameObject tower = _container.InstantiatePrefab(
            towerPrefab,
            place.transform.position,
            Quaternion.identity,
            place.transform
        );

        Debug.Log($"Башня {towerName} успешно построена на позиции {place.transform.position}");
    }
}
}




