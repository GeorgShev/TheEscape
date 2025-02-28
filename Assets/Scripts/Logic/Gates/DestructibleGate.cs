using UnityEngine;

namespace Logic.Gates
{
    public class DestructibleGate : MonoBehaviour , IGatesStatus
    {
        public GameObject HurtBox;
        public GameObject Ui;

        private void Start()
        {
            HurtBox.SetActive(false);
            Ui.SetActive(false);
        }

        public void UpdateStatus()
        {
            HurtBox.SetActive(true);
            Ui.SetActive(true);
        }
    }
}