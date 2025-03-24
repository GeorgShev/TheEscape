using Logic;
using Services.AudioService;
using StaticData;
using UnityEngine;

namespace Enemy
{
    public class EnemyPusher : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public ChasePlayer chasePlayer;
        
        private const string EnemyTag = "Enemy";
        private const string PlayerTag = "Player";
        private const string EnviromentTag = "DestructibleEnviroment";
        
        private IAudioService _audioService;


        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        private void OnCollisionEnter(Collision objectCollision)
        {
            
            
            Vector3 impactVelocity = objectCollision.relativeVelocity;
            float magnitude = Mathf.Max(0f, impactVelocity.magnitude);
            
            if (objectCollision.gameObject.CompareTag(EnemyTag) && magnitude >= 1.7f)
            {
                objectCollision.gameObject.GetComponent<IHealth>()?.TakeDamage(1, Color.white);
                _audioService.PlaySound(AudioTypeId.EnemyColidetEasy, magnitude/10);
            }
            else if (objectCollision.gameObject.CompareTag(EnemyTag) && magnitude < 1.7f)
            {
                _audioService.PlaySound(AudioTypeId.EnemyColidetEasy, magnitude/10);
            }
            else if (objectCollision.gameObject.CompareTag(PlayerTag))
            {
                objectCollision.gameObject.GetComponent<IHealth>()?.TakeDamage(1, Color.white);
            }
            else if (objectCollision.gameObject.CompareTag(EnviromentTag))
            {
                objectCollision.gameObject.GetComponentInParent<DestructibleObjects>().DestroyObject();
                chasePlayer?.Knocked(1f);
                
                
            }
            
        }

    }
}