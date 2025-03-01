using System;
using System.Collections;
using Logic;
using StaticData;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {

        public event Action HealthChanged;
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }

       
        private int _randomValue;
        private PlayerStaticData _playerStaticData;


        public GameObject TextPrefab { get; set; }


        public void Construct(PlayerStaticData playerStaticData)
        {
            _playerStaticData = playerStaticData;
            MaxHP = playerStaticData.MaxHP;
            CurrentHP = playerStaticData.CurrentHP;
            HealthChanged?.Invoke();
        }

       

        public void TakeDamage(int damage, Color color)
        {
                CurrentHP -= damage;
                HealthChanged?.Invoke();
                Debug.Log(CurrentHP);
            //ShowText(damage.ToString(), Color.red);

        }

        public void TakeHP(int Hp)
        {
            CurrentHP += Hp;
            HealthChanged?.Invoke();
            //ShowText(Hp.ToString(), Color.red);
        }





        private void ShowText(string textHP, Color color)
        {
            if (gameObject.activeSelf)
            {
                GameObject textPrefab = Instantiate(TextPrefab, gameObject.transform.position, Quaternion.identity);
                textPrefab.transform.SetParent(gameObject.transform);
                textPrefab.GetComponent<TMP_Text>().SetText(textHP);
                textPrefab.GetComponent<TMP_Text>().color = color;

                StartCoroutine(StartDestroyTimer(textPrefab));
            }
        }
        private IEnumerator StartDestroyTimer(GameObject textPrefab)
        {

            yield return new WaitForSeconds(0.5f);
            Destroy(textPrefab);
        }

    }
}