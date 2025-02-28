using Scripts.Enemy;
using UnityEngine;

namespace Enemy
{
    public class Attack : MonoBehaviour, IAttack
    {

        public float AttackCooldown = 1.5f;
        public float Damage = 1f;
        public float Cleavage = 1f;
        public float EffectiveDistane = 1f;
        public Transform[] ShootStartTransforms;
        public GameObject Laserprefab;

        public Transform _heroTransform;

        private float _attackCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;
        int i = 0;

        public void Construct(Transform heroTransform, float damage)
        {
            _heroTransform = heroTransform;
            Damage = damage;
        }





        private void Update()
        {
            UpdateCooldown();


            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
        }

        private Transform ShootStartPosition()
        {

            Transform startTransform = ShootStartTransforms[i];
            i++;

            if (i == ShootStartTransforms.Length)
            {
                i = 0;
            }


            return startTransform;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _attackCooldown -= Time.deltaTime;
            }

        }
        private void StartAttack()
        {
            //transform.LookAt(_heroTransform);
            OnAttack();
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        private bool CanAttack()
        {
            return  /*_isAttacking &&*/ _attackIsActive && CooldownIsUp() && _heroTransform != null;
        }

        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }


    }
}