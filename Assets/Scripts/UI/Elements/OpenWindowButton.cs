using UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        public WindowId WindowId;
        public Button Button;
        public bool NeedPause;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }



        private void Awake()
        {
            Button.onClick.AddListener(Open);
            //Button = GetComponent<Button>();
        }


        private void Open()
        {
            if (WindowId == WindowId.PauseMenu && _windowService.PauseMenu != null)
            {
                Destroy(_windowService.PauseMenu.gameObject);
            }
            else
            {
                _windowService.Open(WindowId);
            }
            

            //Debug.Log(WindowId);
        }
    }
}