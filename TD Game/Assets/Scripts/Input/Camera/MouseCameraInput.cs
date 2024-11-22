using UnityEngine;
namespace TDGame.Inputs.Cam
{
    public class MouseCameraInput : ICameraInput
    {
    private Vector3 _dragOrigin; 
    private bool _isDragging;

    public Vector3 GetDragDelta()
    {
        if (!_isDragging) return Vector3.zero;

        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 delta = Camera.main.ScreenToWorldPoint(_dragOrigin) - Camera.main.ScreenToWorldPoint(currentMousePosition);

        _dragOrigin = Input.mousePosition;
        return delta;
    }

    public bool IsDragging
    {
        get
        {
            if (Input.GetMouseButtonDown(1))
            {
                _dragOrigin = Input.mousePosition;
                _isDragging = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                _isDragging = false;
            }

            return _isDragging;
        }
    }
    }
}

