using Player;
using Scripts.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class AbilityUI : MonoBehaviour
    {
        public Button AbilityButtonUi;

        public AbilityButton AbilityButton;

        public GameObject Slider;
        
        private AbilityHolder _abilityHolder;




        public void Construct(AbilityHolder abilityHolder)
        {
            _abilityHolder = abilityHolder;
            _abilityHolder.InitHUD(this);
        }

    }
}
