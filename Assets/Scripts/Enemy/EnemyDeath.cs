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
        public ParticleSystem DeathFx;
        public MeshRenderer body;
        public ChasePlayer chasePlayer;
        public Collider boxCollider;
        
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
            
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            float delay = 0.15f;
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
                DeathLogic();
        }

        public void DeathLogic()
        {
            body.enabled = false;
            boxCollider.enabled = false;
            chasePlayer.isKnocked = true;
            _audioService.PlaySound(AudioTypeId.EnemyDie);
            DeathFx.Play();
            Invoke(nameof(HiddenObject), .5f);   
        }

        public void HiddenObject()
        {
            gameObject.SetActive(false);
            boxCollider.enabled = true;
            body.enabled = true;
        }
    }
}