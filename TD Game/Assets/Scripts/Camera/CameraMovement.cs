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

    private ICameraInput _cameraInput;
    private Transform _followTarget;
    private Vector3 _minBounds;
    private Vector3 _maxBounds;
    private float _halfHeight;
    private float _halfWidth;

    private void Start()
    {
        InputChoise();

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

        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("Main Camera not found.");
            enabled = false;
            return;
        }

        _halfHeight = camera.orthographicSize;
        _halfWidth = _halfHeight * camera.aspect;
    }

    private void Update()
    {
        if (_cameraInput == null || !_cameraInput.IsDragging || _followTarget == null) return;

        Vector3 dragDelta = _cameraInput.GetDragDelta() * _dragSpeed;

        Vector3 newPosition = _followTarget.position + dragDelta;

        newPosition.x = Mathf.Clamp(newPosition.x, _minBounds.x + _halfWidth, _maxBounds.x - _halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, _minBounds.y + _halfHeight, _maxBounds.y - _halfHeight);

        _followTarget.position = newPosition;
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
            Debug.Log("Editor platform detected, using MouseCameraInput.");
            cameraInput = new MouseCameraInput();
            break;

        case RuntimePlatform.WindowsEditor:
            Debug.Log("Editor platform detected, using MouseCameraInput.");
            cameraInput = new MouseCameraInput();
            break;

        default:
            throw new Exception("Unsupported platform for camera input.");
    }

    return _cameraInput = cameraInput;
    }
}
}


