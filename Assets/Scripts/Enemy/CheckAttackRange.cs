using Scripts.Enemy;
using UnityEngine;

namespace Enemy
{
    //[RequireComponent(typeof(IAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public IAttack Attack;
        public TriggerObserver TriggerObserver;

        private void Awake()
        {
            Attack = GetComponent<IAttack>();
        }

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            Attack.DisableAttack();
        }


        private void TriggerEnter(Collider obj)
        {
            Attack.EnableAttack();
        }
        private void TriggerExit(Collider obj)
        {
            Attack.DisableAttack();
        }
    }
}