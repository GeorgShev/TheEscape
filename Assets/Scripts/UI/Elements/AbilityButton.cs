using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Elements
{
    public class AbilityButton : MonoBehaviour
    {
        public Image TimerImage;
        public Image AbilityImage;
        public TextMeshProUGUI TimerText;


        private void Start()
        {
            TimerText.alpha = 0;
            TimerImage.fillAmount = 0;
        }

        public void ButtoonCooldown(float current, float max)
        {
            TimerImage.fillAmount = current / max;

            string formattedString = string.Format("{0:F1}", current);

            TimerText.text = formattedString;

            if(current <= 0)
            {
                TimerText.alpha = 0;
            }
            else
            {
                TimerText.alpha = 1;
            }
        }

        public void ButtonActive(float current)
        {
            TimerImage.fillAmount = 0;

            string formattedString = string.Format("{0:F1}", current);

            TimerText.text = formattedString;

            if (current <= 0)
            {
                TimerText.alpha = 0;
            }
            else
            {
                TimerText.alpha = 1;
            }
        }

        public void ButtonReady()
        {
            TimerText.alpha = 0;
            TimerImage.fillAmount = 0;
        }
    }
}
