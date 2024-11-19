using UnityEngine;
namespace TDGame.Inputs
{
    public class CameraMovement : MonoBehaviour
    {
    [SerializeField] private float dragSpeed = 2f; // Скорость передвижения камеры
    private Vector3 _dragOrigin;                  // Начальная точка зажатия
    private bool _isDragging;                     // Флаг для проверки, удерживается ли кнопка мыши

    private void Update()
    {
        HandleCameraDrag();
    }

    private void HandleCameraDrag()
    {
        // Проверяем начало зажатия кнопки мыши
        if (Input.GetMouseButtonDown(1)) // Средняя кнопка мыши (можно поменять на другую, например, Input.GetMouseButtonDown(0) для ЛКМ)
        {
            _dragOrigin = Input.mousePosition; // Сохраняем начальную позицию мыши
            _isDragging = true;
        }

        // Проверяем, если кнопка мыши отпущена
        if (Input.GetMouseButtonUp(1))
        {
            _isDragging = false;
        }

        // Перемещаем камеру при зажатой кнопке
        if (_isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition; // Текущая позиция мыши
            Vector3 difference = _dragOrigin - currentMousePosition; // Разница в позиции мыши

            // Перемещаем камеру на основе разницы в координатах
            Vector3 movement = new Vector3(difference.x, difference.y, 0) * dragSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            // Обновляем начальную точку
            _dragOrigin = currentMousePosition;
        }
    }
}
}

