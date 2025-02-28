using Services;

namespace Infrastructure.State
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState, TPayLoad>(TPayLoad payload) where TState : class, IPayLoadedState<TPayLoad>;
        void Enter<TState>() where TState : class, IState;
    }
}