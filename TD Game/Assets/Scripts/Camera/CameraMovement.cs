using Cinemachine;
using UnityEngine;
using TDGame.GO;
using TDGame.Inputs.Cam;
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

        if (Application.isEditor){
            _cameraInput = new MouseCameraInput();
            Debug.Log("Editor platform detected, using MouseCameraInput.");
        }

        else if (Application.platform == RuntimePlatform.WindowsPlayer) {
            _cameraInput = new MouseCameraInput();
            Debug.Log("Windows platform detected.");
        }

        else if (Application.platform == RuntimePlatform.Android) {
            _cameraInput = new TouchCameraInput();
            Debug.Log("Android platform detected.");    
        }

        else
            Debug.LogError("Unsupported platform for camera input.");


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
            Debug.LogError("Collider wasn't detected.");
            enabled = false;
            return;
        }

        Bounds bounds = boxCollider.bounds;
        _minBounds = bounds.min;
        _maxBounds = bounds.max;

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found.");
            enabled = false;
            return;
        }

        _halfHeight = cam.orthographicSize;
        _halfWidth = _halfHeight * cam.aspect;
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
}
}

