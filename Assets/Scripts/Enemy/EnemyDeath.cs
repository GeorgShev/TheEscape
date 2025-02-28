using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public GameObject DeathFx;

        public event Action Happened;

        private void OnEnable()
        {
            Health.HealthChanged += HealthChanged;
        }

        private void OnDisable()
        {
            Health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if(Health.CurrentHP <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            //Animator.PlayDeath();
            Health.HealthChanged -= HealthChanged;

            //SpawnDeathFx();
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private void SpawnDeathFx()
        {
            GameObject explosion = Instantiate(DeathFx, transform.position, Quaternion.identity);

            Destroy(explosion, 2f);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(.1f);
            gameObject.SetActive(false);
        }
    }
}