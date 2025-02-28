namespace Player.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; set; }

        public void Initialize(State initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState)
        {
            if (CurrentState != newState)
            {
                CurrentState.Exit();
                CurrentState = newState;
                CurrentState.Enter();   
            }
        
        }
    }
}