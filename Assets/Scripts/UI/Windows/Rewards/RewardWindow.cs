using UnityEngine;

namespace UI.Windows.Rewards
{
    public class RewardWindow : WindowBase
    {
        private RewardItem[] _rewardItem;
        private GameObject _player;

        public void Construct()
        {
        }

        private void Awake()
        {
            _rewardItem = GetComponentsInChildren<RewardItem>();
        }

        
        public void FillingItem()
        {
        }

    }
}
