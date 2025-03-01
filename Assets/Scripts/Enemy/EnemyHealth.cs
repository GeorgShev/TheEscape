using System;
using System.Collections;
using Logic;
using TMPro;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {

        public event Action HealthChanged;
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
    

        public GameObject TextPrefab { get; set; }


        public void TakeDamage(int damage, Color color)
        {
            CurrentHP -= damage;

            HealthChanged?.Invoke();

            /*if (TextPrefab)
            {
                ShowText(damage.ToString(), color);
            }*/
            
        }

        public void TakeHP(int HP)
        {
            HealthChanged?.Invoke();

            /*if (TextPrefab)
            {
                ShowText(HP.ToString(), Color.green);
            }*/
            
        }

        private void ShowText(string textHP, Color color)
        {
            GameObject textPrefab = Instantiate(TextPrefab, gameObject.transform.position, Quaternion.identity);
            textPrefab.transform.SetParent(gameObject.transform);
            textPrefab.GetComponent<TMP_Text>().SetText(textHP);
            textPrefab.GetComponent<TMP_Text>().color = color;

            StartCoroutine(StartDestroyTimer(textPrefab));
        }
        private IEnumerator StartDestroyTimer(GameObject textPrefab)
        {

            yield return new WaitForSeconds(0.5f);
            Destroy(textPrefab);
        }
    }
}