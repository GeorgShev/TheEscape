using Infrastructure.State;

namespace UI.Windows.Menu
{
    public class SubLevelContainer : WindowBase
    {


        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        private void Awake()
        {
            //gameObject.SetActive(false);
        }
        

        public void Enable()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
               // Debug.Log("Active");
            }
            else
            {
                gameObject.SetActive(true);
                //Debug.Log("Deactive");
            }
            
        }

        
    }
}
