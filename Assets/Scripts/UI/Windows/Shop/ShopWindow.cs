using Services.Ads;
using Services.PersistentProgressService;
using TMPro;

namespace UI.Windows.Shop
{
    public class ShopWindow: WindowBase
    {
        public TextMeshProUGUI CoinText;
        public RewardedAdItem RewardedAdItem;    

        public void Construct(IAdsService adsService, IPersistentProgressService persistantProgressService)
        {
            base.Construct(persistantProgressService);
            RewardedAdItem.Construct(adsService, persistantProgressService);
        }

        protected override void Initialize()
        {
            RewardedAdItem.Initialize();
            RefreshCoinText();
        }
        protected override void SubscribeUpdates()
        {
            RewardedAdItem.Subscribe();
            PlayerProgress.WorldData.LootData.ChangedValue += RefreshCoinText;
        }

        protected override void Clenup()
        {
            base.Clenup();
            RewardedAdItem.Cleanup();
            PlayerProgress.WorldData.LootData.ChangedValue -= RefreshCoinText;
        }

        private void RefreshCoinText()
        {
             CoinText.text = PlayerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}
