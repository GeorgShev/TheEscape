using UI.Services.Factory;
using UI.Windows.Menu;

namespace UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        //I want to get a link to this window so that I can use it in different places, but am I doing the right thing??
        public ChoiceWindow ChoiceWindow { get; set; }
        public ConfimWindow ConfimWindow { get; set; }
        public PauseMenu PauseMenu { get; set; }


        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;
                case WindowId.LevelsMenu:
                    _uiFactory.CreatePlanetsMenu();
                    break;
                case WindowId.Rewards:
                    _uiFactory.CreateRewardsMenu();
                    break;
                case WindowId.ChoiceWindow:
                    _uiFactory.CreateChoiceWindow(this);
                    break;
                case WindowId.PauseMenu:
                    _uiFactory.CreatePauseMenu(this);
                    break;
                case WindowId.DetailedViewAbilities:
                    _uiFactory.CreateDetailedViewAbility(this);
                    break;
                    case WindowId.Confim:
                    _uiFactory.CreateConfimWindow(this);
                        break;
            }

        }
    }
}
