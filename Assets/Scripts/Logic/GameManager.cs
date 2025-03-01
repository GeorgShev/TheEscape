using Player;
using UnityEngine;

namespace Logic
{
    public class GameManager : MonoBehaviour
    {
        private GameObject _spawnPoint;
        private GameObject _player;

        public void InitSpawner(GameObject spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        public void InitPlayer(GameObject player)
        {
            _player = player;
            _player.GetComponent<PlayerController>().KnockbackAction += PlayerKnocked;
            _player.GetComponent<PlayerController>().RecoverAction += PlayerRecovered;
        }


        private void PlayerKnocked()
        {
            _spawnPoint.GetComponent<WaveManager>().RefreshWaves();
        }

        private void PlayerRecovered()
        {
            _spawnPoint.GetComponent<WaveManager>().ResumeAfterRefresh();
        }
    }
}