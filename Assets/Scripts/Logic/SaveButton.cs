using Services;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class SaveButton : MonoBehaviour
    {
        public Button Button;

        private ISaveLoadService _saveLoadService;

        //private IPersistentProgressService _persistentProgressService;


        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            //_persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();

            Button.onClick.AddListener(SaveProgress);
        }

        public void SaveProgress()
        {
            _saveLoadService.SaveProgress();
        }

    }
}
