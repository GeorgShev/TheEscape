using Services;
using Services.StaticDataService;
using StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic
{
    public class GatesFromLevel : MonoBehaviour
    {
        private IStaticDataService _staticDatatServiece;
        private LevelStaticData _levelStaticData;

        private void Awake()
        {
            _staticDatatServiece = AllServices.Container.Single<IStaticDataService>();
            _levelStaticData = _staticDatatServiece.ForLevel(SceneManager.GetActiveScene().name);
        }

        public void UpdateStatus()
        {
            foreach (EnemySpawnerStaticData enemySpawnerData in _levelStaticData.EnemySpawnerData)
            {

            }
        }
    }
}