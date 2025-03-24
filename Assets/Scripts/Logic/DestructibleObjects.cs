using System.Collections;
using Services;
using Services.AudioService;
using Services.PauseService;
using StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic
{
    public class DestructibleObjects : MonoBehaviour
    {
        public AudioTypeId audioTypeId;

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
            gameObject.SetActive(false);
            
        }
    }
}
