using System;
using System.Collections;
using Logic;
using Services.AudioService;
using Services.PauseService;
using StaticData;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public GameObject DeathFx;
        
        private GameManager _gameManager;
        private IPauseService _pauseService;
        private IAudioService _audioService;

        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void InitPauseService(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void InitAudioService(IAudioService audioService)
        {
            _audioService = audioService;
        }

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
            float delay = 0.1f;
            float timer = 0f;

            while (timer < delay)
            {
                if (!_pauseService.IsPaused)
                {
                    timer += Time.deltaTime;
                }

                yield return null;
            }
                //added score to enemy data
                _gameManager.AddedScore(5);
                _audioService.PlaySound(AudioTypeId.EnemyDie);
                gameObject.SetActive(false);
            
        }
    }
}