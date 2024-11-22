using UnityEngine;

namespace TDGame.Inputs.Cam
{
    public class TouchCameraInput : ICameraInput
    {
    public Vector3 DragDelta { get; private set; }
    public bool IsDragging { get; private set; }

    private Vector3 _dragOrigin;

    public void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _dragOrigin = touch.position;
                    IsDragging = true;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    IsDragging = false;
                    break;

                case TouchPhase.Moved:

                    Vector3 currentTouchPosition = touch.position;
                    DragDelta = Camera.main.ScreenToWorldPoint(_dragOrigin) - Camera.main.ScreenToWorldPoint(currentTouchPosition);
                    _dragOrigin = currentTouchPosition;
                    break;
            }
        }
        else
        {
            DragDelta = Vector3.zero;
            IsDragging = false;
        }
    }

    Vector3 ICameraInput.GetDragDelta() => DragDelta;
    }
}

