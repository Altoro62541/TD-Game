using Cinemachine;
using UnityEngine;
using TDGame.GO;
using TDGame.Inputs.Cam;
using System;

namespace TDGame.Cam
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float _dragSpeed = 0.5f;
    [SerializeField] private CameraCollider _boundaryCollider;

    // Параметры для масштабирования (зумирования)
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minZoom = 2f;
    [SerializeField] private float _maxZoom = 10f;

    private ICameraInput _cameraInput;
    private Transform _followTarget;
    private Vector3 _minBounds;
    private Vector3 _maxBounds;
    private float _halfHeight;
    private float _halfWidth;
    private Camera _mainCamera;

    private void Start()
    {
        _cameraInput = InputChoise();

        _followTarget = _virtualCamera.Follow;
        if (_followTarget == null)
        {
            Debug.LogError("No Follow object detected.");
            enabled = false;
            return;
        }

        var boxCollider = _boundaryCollider?.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            enabled = false;
            throw new Exception("Collider wasn't detected.");
        }
        Bounds bounds = boxCollider.bounds;
        _minBounds = bounds.min;
        _maxBounds = bounds.max;

        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("Main Camera not found.");
            enabled = false;
            return;
        }

        _halfHeight = _mainCamera.orthographicSize;
        _halfWidth = _halfHeight * _mainCamera.aspect;
    }

    private void Update()
    {
        // Обработка перетаскивания камеры
        if (_cameraInput != null && _cameraInput.IsDragging && _followTarget != null)
        {
            Vector3 dragDelta = _cameraInput.GetDragDelta() * _dragSpeed;
            Vector3 newPosition = _followTarget.position + dragDelta;

            newPosition.x = Mathf.Clamp(newPosition.x, _minBounds.x + _halfWidth, _maxBounds.x - _halfWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, _minBounds.y + _halfHeight, _maxBounds.y - _halfHeight);

            _followTarget.position = newPosition;
        }

        // Обработка масштабирования (зум)
        float zoomDelta = GetZoomDelta();
        if (!Mathf.Approximately(zoomDelta, 0f))
        {
            float newSize = Mathf.Clamp(_virtualCamera.m_Lens.OrthographicSize - zoomDelta * _zoomSpeed, _minZoom, _maxZoom);
            _virtualCamera.m_Lens.OrthographicSize = newSize;

            // Пересчитываем половинные размеры экрана с учётом нового зума
            _halfHeight = newSize;
            _halfWidth = newSize * _mainCamera.aspect;
        }

        // После любых изменений (перетаскивания или зума) обязательно ограничиваем позицию цели камеры,
        // чтобы она не выходила за пределы заданного коллайдера.
        if (_followTarget != null)
        {
            ClampFollowTargetPosition();
        }
    }

    /// <summary>
    /// Ограничивает позицию цели камеры так, чтобы при отображении области не выходить за границы.
    /// </summary>
    private void ClampFollowTargetPosition()
    {
        Vector3 clampedPosition = _followTarget.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, _minBounds.x + _halfWidth, _maxBounds.x - _halfWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, _minBounds.y + _halfHeight, _maxBounds.y - _halfHeight);
        _followTarget.position = clampedPosition;
    }

    private ICameraInput InputChoise()
    {
        ICameraInput cameraInput = null;
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
                Debug.Log("Windows platform detected.");
                cameraInput = new MouseCameraInput();
                break;
            case RuntimePlatform.Android:
                Debug.Log("Android platform detected.");
                cameraInput = new TouchCameraInput();
                break;
            case RuntimePlatform.WebGLPlayer:
            case RuntimePlatform.WindowsEditor:
                Debug.Log("Editor platform detected, using MouseCameraInput.");
                cameraInput = new MouseCameraInput();
                break;
            default:
                throw new Exception("Unsupported platform for camera input.");
        }
        return _cameraInput = cameraInput;
    }

    /// <summary>
    /// Возвращает величину изменения зума.
    /// Для десктопа используется колесо мыши, для мобильных устройств – пинч-жест.
    /// </summary>
    private float GetZoomDelta()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetAxis("Mouse ScrollWheel");
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 prevTouchZeroPos = touchZero.position - touchZero.deltaPosition;
            Vector2 prevTouchOnePos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (prevTouchZeroPos - prevTouchOnePos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMagnitude - prevMagnitude;
            return difference * 0.01f;
        }
        return 0f;
#else
        return 0f;
#endif
    }
}
}


