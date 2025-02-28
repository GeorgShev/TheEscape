namespace Infrastructure.State
{
    public class GameLoopState :IState
    {
        public GameLoopState(GameStateMachine gameStateMachine)
        {

        }

        void IState.Enter()
        {
        }

        void IExitableState.Exit()
        {
        }
    }
}