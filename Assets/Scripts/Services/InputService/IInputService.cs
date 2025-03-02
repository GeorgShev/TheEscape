using UnityEngine;

namespace Services.InputService
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsDashButtonUp();
    }
}