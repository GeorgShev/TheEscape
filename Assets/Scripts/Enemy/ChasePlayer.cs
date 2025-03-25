using System.Collections;
using Services.PauseService;
using UnityEngine;

namespace Enemy
{
    public class ChasePlayer : MonoBehaviour
    {
        public float moveSpeed = 5f; 
        public float accelerationForce = 10f;
        public float rotationSpeed = 100f; 
        public float maxSpeed = 10f;
        public float brakeForce = 20f;
        public bool isKnocked;
        private Transform _player; 
        private Rigidbody _rigidbody;
        private bool _isBraking;
        private IPauseService _pauseService;
        private bool _setPaused;
        private Vector3 _linearVelocity;
        
        
        
        
        public void Construct(GameObject player, IPauseService pauseService)
        {
            _player = player.transform;
            _pauseService = pauseService;
        }

        public void Knocked(float duration)
        {
            StartCoroutine(KnockedTimer(duration));
        }

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

        }

        void FixedUpdate()
        {
            if (_pauseService != null && _pauseService.IsPaused || isKnocked)
            {
                SetPaused();
                return;
            }
            else
            {
                ResumeFromPause();
            }

            if (!_player)
            {
                return;
            }

            
            Vector3 direction = (_player.position - transform.position).normalized;
            direction.y = 0; 

           
            RotateTowardsPlayer(direction);

           
            MoveOrBrake(direction);
        }

        private void SetPaused()
        {
            if (!_setPaused)
            {
                _setPaused = true;
                _linearVelocity = _rigidbody.linearVelocity;
                _rigidbody.isKinematic = true;
                _rigidbody.linearVelocity = Vector3.zero;
            }
            
            
        }

        private void ResumeFromPause()
        {
            if (_setPaused)
            {
                _setPaused = false;
                _rigidbody.isKinematic = false;
                _rigidbody.linearVelocity = _linearVelocity;
            }
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
                _rigidbody.AddForce(transform.forward * accelerationForce, ForceMode.Acceleration);
                _isBraking = false;
            }
            else 
            {
                Brake();
            }

            
            if (_rigidbody.linearVelocity.magnitude > maxSpeed)
            {
                _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * maxSpeed;
            }
        }

        void Brake()
        {
            if (!_isBraking)
            {
                _rigidbody.AddForce(-_rigidbody.linearVelocity.normalized * brakeForce, ForceMode.Acceleration);
                _isBraking = true;
            }
        }
        
        private IEnumerator KnockedTimer(float duration)
        {
            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                if (isKnocked == false)
                {
                    isKnocked = true;
                }

                yield return null; 
            }
            isKnocked = false;
        }
    }
}