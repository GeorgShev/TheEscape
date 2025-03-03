using Player;
using UnityEngine;

namespace Logic
{
    public class GameManager : MonoBehaviour
    {
        private GameObject _spawnPoint;
        private GameObject _player;
        private WaveManager _waveManager;

        public void InitSpawner(GameObject spawnPoint)
        {
            _spawnPoint = spawnPoint;
            _waveManager = _spawnPoint.GetComponent<WaveManager>();
            _waveManager.StartAfterInitPlayer();
        }

        public void InitPlayer(GameObject player)
        {
            _player = player;
            _player.GetComponent<PlayerController>().KnockbackAction += PlayerKnocked;
            _player.GetComponent<PlayerController>().RecoverAction += PlayerRecovered;
        }


        private void PlayerKnocked()
        {
            _waveManager.RefreshWaves();
        }

        private void PlayerRecovered()
        {
            _waveManager.ResumeAfterRefresh();
        }
    }
}