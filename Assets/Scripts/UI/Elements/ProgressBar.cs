using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Elements
{
    public class ProgressBar : MonoBehaviour
    {
        public Image ImageCurrent;

        public void SetValue(float current, float max)
        {
            ImageCurrent.fillAmount = current / max;
        }
    }
}
