using System.Collections;
using Services;
using Services.AudioService;
using Services.PauseService;
using StaticData;
using UnityEngine;

namespace Logic
{
    public class DestructibleObjects : MonoBehaviour
    {
        public AudioTypeId audioTypeId;
        public ParticleSystem DeathFx;
        public MeshRenderer body;
        public Collider Collider;

        private IPauseService _pauseService;
        private IAudioService _audioService;
        private void Start()
        {
            _pauseService = AllServices.Container.Single<IPauseService>();
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        public void DestroyObject()
        {
            _audioService.PlaySound(audioTypeId);
            StartCoroutine(DestroyTimer());
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
            
            _audioService.PlaySound(AudioTypeId.EnemyDie);
            DeathLogic();

        }
        
        public void DeathLogic()
        {
            body.enabled = false;
            Collider.enabled = false;
            _audioService.PlaySound(AudioTypeId.EnemyDie);
            DeathFx.Play();
            Invoke(nameof(HiddenObject), .5f);   
        }

        public void HiddenObject()
        {
            gameObject.SetActive(false);
            Collider.enabled = true;
            body.enabled = true;
        }
    }
}
