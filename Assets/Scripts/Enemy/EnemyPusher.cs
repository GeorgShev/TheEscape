using Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyPusher : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public ChasePlayer chasePlayer;
        
        private const string EnemyTag = "Enemy";
        private const string PlayerTag = "Player";
        private const string EnviromentTag = "DestructibleEnviroment";
        
        
        private void OnCollisionEnter(Collision objectCollision)
        {
            
            
            Vector3 impactVelocity = objectCollision.relativeVelocity;
            float magnitude = Mathf.Max(0f, impactVelocity.magnitude);
            
            if (objectCollision.gameObject.CompareTag(EnemyTag) && magnitude >= 1.7f)
            {
                //Debug.LogError(other.gameObject.name);
                //Debug.LogError(magnitude.ToString());
                objectCollision.gameObject.GetComponent<IHealth>()?.TakeDamage(1, Color.white);
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