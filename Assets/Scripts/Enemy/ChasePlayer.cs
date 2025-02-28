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

            // Направление к игроку
            Vector3 direction = (_player.position - transform.position).normalized;
            direction.y = 0; // Игнорируем ось Y

            // Поворот в сторону игрока
            RotateTowardsPlayer(direction);

            // Движение вперед или торможение
            MoveOrBrake(direction);
        }

        void RotateTowardsPlayer(Vector3 direction)
        {
            // Вычисляем целевой поворот
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            // Плавно поворачиваем врага
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        void MoveOrBrake(Vector3 direction)
        {
            // Угол между направлением к игроку и текущим направлением врага
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle < 90f) // Если игрок впереди
            {
                // Движение вперед
                rb.AddForce(transform.forward * accelerationForce, ForceMode.Acceleration);
                isBraking = false;
            }
            else // Если игрок сзади
            {
                // Торможение
                Brake();
            }

            // Ограничение скорости
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }

        void Brake()
        {
            if (!isBraking)
            {
                // Применяем силу торможения
                rb.AddForce(-rb.linearVelocity.normalized * brakeForce, ForceMode.Acceleration);
                isBraking = true;
            }
        }
    }
}