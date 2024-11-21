using UnityEngine;
namespace TDGame.Inputs.Cam
{
    public interface ICameraInput
    {
        public Vector3 GetDragDelta();
        public bool IsDragging { get; }
    }
}

