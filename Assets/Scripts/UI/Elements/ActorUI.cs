using Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar hpBar;
        public PointCounter pointCounter;
        //public TextMeshProUGUI TextMeshProUGUI;

        private IHealth _heroHealth;


        public void Construct(IHealth health)
        {
            _heroHealth = health;
            UpdateHpBar();
            hpBar.gameObject.SetActive(true);
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        public void UpdatePointCounter(int point)
        {
            pointCounter.UpdateCounter(point);
        }


        private void Awake()
        {
            hpBar.gameObject.SetActive(false);
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
            hpBar.SetValue(_heroHealth.CurrentHP, _heroHealth.MaxHP);
            /*if(TextMeshProUGUI != null)
            {
                TextMeshProUGUI.SetText(_heroHealth.CurrentHP.ToString());
            }*/
        }
        
        
    }
}