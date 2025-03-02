using UnityEngine;

namespace Enemy
{
    public class ChasePlayer : MonoBehaviour
    {
        public float moveSpeed = 5f; // Скорость движения
        public float accelerationForce = 10f;
        public float rotationSpeed = 100f; // Скорость поворота
        public float maxSpeed = 10f; // Максимальная скорость
        public float brakeForce = 20f;
        private Transform _player; // Цель (персонаж)
        private Rigidbody rb;
        private bool isBraking = false;
        public void Construct(GameObject player)
        {
            _player = player.transform;
        }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();

        }

        void FixedUpdate()
        {
            if (_player == null) return;

            
            Vector3 direction = (_player.position - transform.position).normalized;
            direction.y = 0; 

           
            RotateTowardsPlayer(direction);

           
            MoveOrBrake(direction);
        }

        void RotateTowardsPlayer(Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        void MoveOrBrake(Vector3 direction)
        {
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle < 90f)
            {
                rb.AddForce(transform.forward * accelerationForce, ForceMode.Acceleration);
                isBraking = false;
            }
            else 
            {
                Brake();
            }

            
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }

        void Brake()
        {
            if (!isBraking)
            {
                rb.AddForce(-rb.linearVelocity.normalized * brakeForce, ForceMode.Acceleration);
                isBraking = true;
            }
        }
    }
}