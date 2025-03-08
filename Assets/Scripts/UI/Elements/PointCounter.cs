using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class PointCounter : MonoBehaviour
    {
        public TextMeshProUGUI counter;
        private int _currentPoint = 0;

        private void Start()
        {
            UpdateCounter(_currentPoint);
        }

        public void UpdateCounter(int point)
        {
            _currentPoint = point;
            counter.text = $"{_currentPoint}";
        }
    }
}
