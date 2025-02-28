using Infrastructure.State;
using Logic;
using Services;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine GameStateMachine;
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}