namespace Infrastructure.State
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}