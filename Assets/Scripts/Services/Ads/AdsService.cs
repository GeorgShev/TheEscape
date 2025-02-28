using System;

namespace Services.Ads
{
    public class AdsService : IAdsService /* IUnityAdsListener */
    {
        public bool IsRewardedVideoReady => true;

        public int Reward => 10;

        public event Action RewardedVideoReady;

        public void Initialize()
        {
            //Debug.Log("AsService Initialize");
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            //Debug.Log("AsService Show Video");
        }
    }
}
