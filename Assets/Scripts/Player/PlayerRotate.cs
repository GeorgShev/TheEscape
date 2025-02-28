using UnityEngine;

namespace Player
{
    public class PlayerRotate : MonoBehaviour
    {
        public float speed = 15f;
        private Vector3 _positionToLook;
        private Vector3 _movementVector;

        void Update()
        {
            //RotateTowards();
        }

        public void UpdateMovementVector(Vector3 movementVector)
        {
            _movementVector = movementVector;
        }

        private void RotateTowards()
        {
            transform.rotation = SmoothedRotation(transform.rotation, _movementVector);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private float SpeedFactor()
        {
            return speed * Time.deltaTime;
        }

        private Quaternion TargetRotation(Vector3 positionToLook)
        {
            return Quaternion.LookRotation(positionToLook);
        }
    }
}