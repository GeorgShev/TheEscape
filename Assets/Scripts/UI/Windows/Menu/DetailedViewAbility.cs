using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace UI.Windows.Menu
{

    public class DetailedViewAbility : WindowBase
    {
        public Image Icon;
        public Outline Outline;
        public LocalizeStringEvent AbilityName;
        public LocalizeStringEvent AbilityDescription;

        public void Construct(Sprite sprite, string abilityName, string abilityDescription, Color color)
        {
            Icon.sprite = sprite;
            Outline.effectColor = color;
            AbilityName.StringReference.TableEntryReference = abilityName;            
            AbilityDescription.StringReference.TableEntryReference = abilityDescription;

            AbilityName.RefreshString();
            AbilityDescription.RefreshString();
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
