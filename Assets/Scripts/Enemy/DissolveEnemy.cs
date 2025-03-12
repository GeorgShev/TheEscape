using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class DissolveEnemy : MonoBehaviour
    {
        public Renderer dissolveMaterial;
        public ChasePlayer chasePlayer;
        public float duration = .5f; 
        
        private float parameterValue = -1f; 

        public void HideObject()
        {
            StartCoroutine(LerpParameterUp());
            chasePlayer.isKnocked = true;
        }

        public void ShowObject()
        {
            dissolveMaterial.material.SetFloat( "_AlphaThresold" , 1.1f);
            StartCoroutine(LerpParameterDown());
            chasePlayer.isKnocked = true;
        }

        private IEnumerator LerpParameterUp()
        {
            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                
                dissolveMaterial.material.SetFloat( "_AlphaThresold" , Mathf.Lerp(-1f, 1f, timer / duration));

                yield return null; 
            }

            
            dissolveMaterial.material.SetFloat( "_AlphaThresold" , 1.1f);
            chasePlayer.isKnocked = false;
            gameObject.SetActive(false);
        }

        private IEnumerator LerpParameterDown()
        {
            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                
                dissolveMaterial.material.SetFloat( "_AlphaThresold" , Mathf.Lerp(1f, -1f, timer / duration));

                yield return null; 
            }
            
            dissolveMaterial.material.SetFloat( "_AlphaThresold" , -1f);
            chasePlayer.isKnocked = false;
        }
    }
}
