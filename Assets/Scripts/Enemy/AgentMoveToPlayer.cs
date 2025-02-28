using System.Collections;
using Scripts.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        public  NavMeshAgent _agent;
        private GameObject _player;
        private Transform _heroTransfom;
        private Vector3 _position;
        private float _updateTime = 0.1f;


        internal void Construct(GameObject heroTransform)
        {
            _player = heroTransform;
        }

        private void OnEnable()
        {
            StartCoroutine(FollowTarget());
        }

        private void Update()
        {
            if (_player)
            {
                _heroTransfom = _player.transform;
                _position = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            }
            
           
        }



        private IEnumerator FollowTarget()
        {
            WaitForSeconds wait = new WaitForSeconds(_updateTime);


            while (enabled)
            {
                if (_heroTransfom != null)
                {
                    _agent.SetDestination(_position);
                }

                yield return wait;
            }
        }
    }
}