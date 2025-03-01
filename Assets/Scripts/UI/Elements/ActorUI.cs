using System;
using Logic;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;
        //public TextMeshProUGUI TextMeshProUGUI;

        private IHealth _heroHealth;



        public void Construct(IHealth health)
        {
            _heroHealth = health;
            UpdateHpBar();
            HpBar.gameObject.SetActive(true);
            _heroHealth.HealthChanged += UpdateHpBar;
        }

       

        private void Awake()
        {
            HpBar.gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            if (_heroHealth != null)
            {
                _heroHealth.HealthChanged -= UpdateHpBar;
            }
        }

        private void UpdateHpBar()
        {
            HpBar.SetValue(_heroHealth.CurrentHP, _heroHealth.MaxHP);
            /*if(TextMeshProUGUI != null)
            {
                TextMeshProUGUI.SetText(_heroHealth.CurrentHP.ToString());
            }*/
            
        }

        

    }
}