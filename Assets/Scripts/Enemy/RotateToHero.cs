using UnityEngine;

namespace Enemy
{
    public class RotateToHero : Follow
    {
        public float speed = 15f;
        public bool IsCollided;


        private Transform _heroTransform;
        private Vector3 _positionToLook;
        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }



        void Update()
        {
            if(Initialized() && IsCollided)
            {
                RotateTowardshero();
            }
        }

        private void RotateTowardshero()
        {
            UpdatePositionLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, positionDiff.y, positionDiff.z);
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

       

        private bool Initialized()
        {
            return _heroTransform != null;
        }
        
    }
}