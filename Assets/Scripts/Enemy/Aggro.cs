using System.Collections;
using Scripts.Enemy;
using UnityEngine;

namespace Enemy
{
    public class Aggro : MonoBehaviour
    {
        public float cooldown = 1f;

        private TriggerObserver _triggerObserver;
        private AgentMoveToPlayer _follow;
        private Coroutine _aggroCooutine;
        private RotateToHero _rotateToHero;
        private Ray _ray;

        private bool _hasAggroTargget;

        private void Awake()
        {
            _triggerObserver = GetComponentInChildren<TriggerObserver>();
            _follow = GetComponentInChildren<AgentMoveToPlayer>();
            _rotateToHero = GetComponentInChildren<RotateToHero>();

        }

        private void Start()
        {

            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }


        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTargget)
            {
                _hasAggroTargget = true;
                _rotateToHero.IsCollided = _hasAggroTargget;

                
                
                StopAggroCorutine();
                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTargget)
            {
                _hasAggroTargget = false;
                _rotateToHero.IsCollided = _hasAggroTargget;

                
                _aggroCooutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(cooldown);

            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            if (_follow)
            {  
                _follow.enabled = true;
            }
        }
        private void SwitchFollowOff()
        {
            if (_follow)
            {
                _follow.enabled = false;
            }
                        
        }
        private void StopAggroCorutine()
        {
            if ( _aggroCooutine != null)
            {
                StopCoroutine(_aggroCooutine);
                _aggroCooutine = null;
            }
        }

    }
}