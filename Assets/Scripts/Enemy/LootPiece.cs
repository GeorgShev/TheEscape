using System.Collections;
using Data;
using Services.PersistentProgressService;
using TMPro;
using UnityEngine;
using Logic;
using UniqueId = Logic.UniqueId;

namespace Enemy
{
    public class LootPiece : MonoBehaviour, ISavedProgress
    {
        public GameObject Model;
        public GameObject PickupFxPrefab;
        public GameObject PickupPopup;
        public TextMeshPro LootText;

        private float _speed = 1.5f;
        private Loot _loot;
        private WorldData _worldData;
        private Transform _player;


        private bool _pickedUp;
        private bool _loadedFromProgress;
        private string _id;


        public void Construct(WorldData worldData, GameObject player)
        {
            _worldData = worldData;
            _player = player.transform;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _id = GetComponent<UniqueId>().Id;

            LootPieceData data = progress.WorldData.LootData.LootPieceOnScene.Dictionary[_id];
            Initialize(data.Loot);
            transform.position = data.Position.AsUnityVector();

            _loadedFromProgress = true;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void Start()
        {
            if (!_loadedFromProgress)
            {
                _id = GetComponent<UniqueId>().Id;
            }
        }

        private void Update()
        {
            if (!_pickedUp && _player != null)
            {
                transform.position = Vector3.Lerp(transform.position, _player.position, _speed * Time.deltaTime);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_pickedUp)
            {
                _pickedUp = true;
                Pickup();
            }

        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_pickedUp)
            {
                return;
            }

            LootPieceDataDictionary lootPieceOnScene = playerProgress.WorldData.LootData.LootPieceOnScene;

            if (!lootPieceOnScene.Dictionary.ContainsKey(_id))
            {
                lootPieceOnScene.Dictionary.Add(_id, new LootPieceData(transform.position.AsVectorData(), _loot));
            }
        }

        private void Pickup()
        {
            //UpdateWorldData();
            HideModel();
            ReceivingAward();
            //PlayPickupFx();
            //ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        public void ReceivingAward()
        {
            int random = Random.Range(0, 6);
            

            if (random < 5)
            {
                int randomHealth = Random.Range(1, 5);
                _player.GetComponent<IHealth>().TakeHP(randomHealth);
            }
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideModel()
        {
            Model.SetActive(false);
        }

        private void PlayPickupFx()
        {
            Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);
        }

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickupPopup.SetActive(true);
        }
        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}