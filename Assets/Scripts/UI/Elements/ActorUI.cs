using Logic;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;
        public TextMeshProUGUI TextMeshProUGUI;

        private IHealth _heroHealth;



        public void Construct(IHealth health)
        {
            _heroHealth = health;
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void Awake()
        {
            // Logic for enemies
            IHealth health = GetComponent<IHealth>();

            if (health != null)
            {
                Construct(health);
            }
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
            if(TextMeshProUGUI != null)
            {
                TextMeshProUGUI.SetText(_heroHealth.CurrentHP.ToString());
            }
            
        }

        

    }
}