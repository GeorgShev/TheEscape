using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace UI.Windows.Rewards
{
    public class RewardItem : MonoBehaviour
    {
        public Button SelectAbilityButton;

        public Image Icon;
        public LocalizeStringEvent AbilityName;
        public LocalizeStringEvent AbilityDescription;
        private Outline _outline;
        private bool _secondary;

        [SerializeField] private Button TakeAbilityButton;



        private void Awake()
        {
            TakeAbilityButton.onClick.AddListener(() => ClickButton());
            _outline = GetComponent<Outline>();
        }

        private void ClickButton()
        {
        }


    }
}

