using UnityEngine;

namespace Player.StateMachine
{
    public abstract class State
    {
        protected PlayerController Player;
        protected Animator _animator;
        protected HashAnimationNames _animationNames = new HashAnimationNames();

        protected State(PlayerController player, Animator animator)
        {
            Player = player;
            _animator = animator;
        }
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }
    }
}