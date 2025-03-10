using System.Collections;
using System.Collections.Generic;
using TDGame.Towers;
using UnityEngine;

namespace TDGame.Inputs
{
    public class UpgradeInputHandler : MonoBehaviour
    {
        private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        // При нажатии клавиши U пытаемся улучшить башню под курсором
        if (Input.GetKeyDown(KeyCode.U))
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                // Ищем компонент TowerUpgrade у объекта или его родителя
                TowerUpgrade towerUpgrade = hit.collider.GetComponentInParent<TowerUpgrade>();
                if (towerUpgrade != null)
                {
                    towerUpgrade.TryUpgrade();
                }
                else
                {
                    Debug.Log("Под курсором нет башни, которую можно улучшить.");
                }
            }
        }
    }
    }
}

