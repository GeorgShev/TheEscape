
using UnityEngine;

namespace Player
{
    public class HashAnimationNames
    {
        protected HashAnimationNames animBase;
    
        public int IdleHash = Animator.StringToHash("Idle");
        public int WalkHash = Animator.StringToHash("Walk");
        public int RunHash = Animator.StringToHash("Run");
        public int DashHash = Animator.StringToHash("Dash");
    }
}
