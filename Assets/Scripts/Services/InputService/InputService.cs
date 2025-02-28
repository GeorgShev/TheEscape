using UnityEngine;

namespace Services.InputService
{
    public abstract class InputService : IInputService
    {
        public Vector2 Axis { get; }
        
        public bool IsDashButtonUp()
        {
            throw new System.NotImplementedException();
        }

        public bool IsJumpButtonUp()
        {
            throw new System.NotImplementedException();
        }
    }
}