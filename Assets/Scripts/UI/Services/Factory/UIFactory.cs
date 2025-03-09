using System.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.State;
using Services.Ads;
using Services.PauseService;
using Services.PersistentProgressService;
using Services.StaticDataService;
using StaticData;
using UI.Services.Windows;
using UI.Windows.Menu;
using UI.Windows.Rewards;
using UI.Windows.Shop;
using UnityEngine;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {

        private const string UIRootPath = "UIRoot";
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _persistantProgressService;
        private readonly IAdsService _adsService;
        private readonly IPauseService _pauseService;
        private readonly IGameStateMachine _gameStateMachine;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IPauseService pauseService,
            IPersistentProgressService persistantProgressService,
            IAdsService adsService,
            IGameStateMachine gameStateMachine)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _pauseService = pauseService;
            _persistantProgressService = persistantProgressService;
            _adsService = adsService;
            _gameStateMachine = gameStateMachine;
        }

        public void CreatePlanetsMenu( )
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.LevelsMenu);
            LevelsMenu levlesmenu = Object.Instantiate(config.Prefab) as LevelsMenu;
            levlesmenu.Construct(_gameStateMachine, _staticDataService);
        }

        public void CreateRewardsMenu()
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.Rewards);
            RewardWindow rewardWindow = Object.Instantiate(config.Prefab, _uiRoot) as RewardWindow;

        }

        public void CreatePauseMenu(IWindowService windowService)
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.PauseMenu);
            PauseMenu pauseMenu = Object.Instantiate(config.Prefab, _uiRoot) as PauseMenu;
            pauseMenu.Construct(_gameStateMachine, windowService, _pauseService);
            windowService.PauseMenu = pauseMenu;
        }

        public void CreateDetailedViewAbility(IWindowService windowService)
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.DetailedViewAbilities);
            DetailedViewAbility detailedViewAbility = Object.Instantiate(config.Prefab, _uiRoot) as DetailedViewAbility;
        }

        public void CreateShop()
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.Shop);
            ShopWindow shopWondow = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            shopWondow.Construct(_adsService, _persistantProgressService);
        }

        public void CreateChoiceWindow(IWindowService windowService)
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.ChoiceWindow);
            ChoiceWindow choiceWindow = Object.Instantiate(config.Prefab, _uiRoot) as ChoiceWindow;
            windowService.ChoiceWindow = choiceWindow;
        }

        public void CreateConfimWindow(IWindowService windowService)
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.Confim);
            ConfimWindow confimWindow = Object.Instantiate(config.Prefab, _uiRoot) as ConfimWindow;
            windowService.ConfimWindow = confimWindow;
        }
        public async Task CreateUIRoot()
        {
            GameObject root = await _assetProvider.Instantiate(UIRootPath);
            _uiRoot = root.transform;
        }

        
    }
}
