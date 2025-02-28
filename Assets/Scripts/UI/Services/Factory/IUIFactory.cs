using System.Threading.Tasks;
using Services;
using UI.Services.Windows;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {

        void CreatePlanetsMenu();
        void CreateRewardsMenu();
        void CreatePauseMenu(IWindowService windowService);
        void CreateDetailedViewAbility(IWindowService windowService);
        void CreateShop();
        void CreateChoiceWindow(IWindowService windowService);
        void CreateConfimWindow(IWindowService windowService);
        Task CreateUIRoot();
    }
}
