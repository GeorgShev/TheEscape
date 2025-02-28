using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper gameBootstrapperPrefab;
        private void Awake()
        {
            var bootstrapper = FindAnyObjectByType<GameBootstrapper>();
            
            if(bootstrapper == null)
            {
                Instantiate(gameBootstrapperPrefab);
            }
        }
    }
    
}


