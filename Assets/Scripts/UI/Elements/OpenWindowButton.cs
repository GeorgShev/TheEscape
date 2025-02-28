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
            _windowService.Open(WindowId);

            if(NeedPause)
            {
                Time.timeScale = 0f;
            }

            if(WindowId == WindowId.LevelsMenu)
            {

            }

            //Debug.Log(WindowId);
        }
    }
}