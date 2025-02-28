using Scripts.Enemy;
using UnityEngine;

namespace Enemy
{
    public class AgentSearchPlayer : Follow
    {
        private Transform _heroTransfom;


        internal void Construct(Transform heroTransform)
        {
            _heroTransfom = heroTransform;
        }       
    }
}