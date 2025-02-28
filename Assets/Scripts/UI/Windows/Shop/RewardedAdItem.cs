using Services.Ads;
using Services.PersistentProgressService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        public Button ShowAdButton;

        public GameObject[] AdActiveObjects;
        public GameObject[] AdInactiveObjects;

        private IAdsService _adsService;
        private IPersistentProgressService _persistentProgressService;

        public void Construct(IAdsService adsService, IPersistentProgressService persistentProgressService)
        {
            _adsService = adsService;
            _persistentProgressService = persistentProgressService;
        }

        public void Initialize()
        {
            ShowAdButton.onClick.AddListener(OnShowAdClicked);
            RefreshAvailableAd();
        }

        public void Subscribe()
        {
            _adsService.RewardedVideoReady += RefreshAvailableAd;
        }


        public void Cleanup()
        {
            _adsService.RewardedVideoReady -= RefreshAvailableAd;
        }


        private void RefreshAvailableAd()
        {
            _adsService.ShowRewardedVideo(OnVideoFinished);
        }

        private void OnVideoFinished()
        {
            _persistentProgressService.Progress.WorldData.LootData.Add(_adsService.Reward);
        }

        private void OnShowAdClicked()
        {
            bool videoReady = _adsService.IsRewardedVideoReady;

            foreach(GameObject adActiveObject in AdActiveObjects)
            {
                adActiveObject.SetActive(videoReady);
            }

            foreach(GameObject adInactiveObject in AdInactiveObjects)
            {
                adInactiveObject.SetActive(!videoReady);
            }
        }
       
    }
}
