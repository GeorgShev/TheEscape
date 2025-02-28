using Infrastructure.State;
using Services.StaticDataService;

namespace UI.Windows.Menu
{
    public class LevelsMenu : WindowBase
    {
        public IGameStateMachine GameStateMachine;
        public IStaticDataService StaticDataService;

        public void Construct(IGameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            GameStateMachine = gameStateMachine;
            StaticDataService = staticDataService;
        }
    }
}
