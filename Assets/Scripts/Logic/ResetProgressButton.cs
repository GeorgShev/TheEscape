using Data;
using Services;
using Services.PersistentProgressService;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class ResetProgressButton : MonoBehaviour
    {
        public Button Button;
        public string InitialLevel = "MainMenu";



        private IPersistentProgressService _persistentProgressService;


        private void Awake()
        {

            _persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();

            Button.onClick.AddListener(ResetProgress);
        }

        public void ResetProgress()
        {
            _persistentProgressService.Progress = NewProgress();
        }

        public PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: InitialLevel);



            return progress;
        }

    }
}
