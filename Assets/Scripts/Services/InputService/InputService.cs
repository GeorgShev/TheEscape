using UnityEngine;

namespace Services.InputService
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Jump"; 
        
        
        public abstract Vector2 Axis { get; }
        
        public bool IsDashButtonUp()
        {
            return SimpleInput.GetButtonDown(Button);
        }
        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}