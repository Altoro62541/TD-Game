using System;
using System.Collections;
using System.Collections.Generic;
using TDGame.Towers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PurchasePanel : MonoBehaviour
{
    [SerializeField] private Button _rocketButton;
    [SerializeField] private Button _projectileButton;

    private RectTransform _rectTransform;
    public bool IsActive => gameObject.activeSelf;

    // Событие выбора башни: подписывается TowerPlacement
    public event Action<int> OnTowerSelectedEvent;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        HidePanel();

        _rocketButton.onClick.AddListener(() => OnTowerSelected(0));
        _projectileButton.onClick.AddListener(() => OnTowerSelected(1));
    }

    private void OnTowerSelected(int towerType)
    {
        OnTowerSelectedEvent?.Invoke(towerType);
    }

    // Перевод мировых координат в локальные координаты канваса
    public void ShowPanel(Vector3 worldPosition)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        RectTransform canvasRect = _rectTransform.parent.GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, Camera.main, out localPoint);

        _rectTransform.localPosition = localPoint;
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
