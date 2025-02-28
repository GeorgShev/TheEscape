using UnityEngine;

namespace CameraScripts
{
    class CameraFollow : MonoBehaviour
    {
        public float RotationAngleX;
        public float Distance;
        public float OffsetY;

        [SerializeField]
        private Transform _followingTarget;        

        private void LateUpdate()
        {
            if (_followingTarget == null)
            {
                return;
            }

                Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
                Vector3 followingPosition = FollowingPoinPosition();
                Vector3 position = rotation * new Vector3(0, 0, -Distance) + followingPosition;

                transform.rotation = rotation;
                transform.position = position;
            
        }

        public void FollowObject(GameObject followingObject)
        {
            _followingTarget = followingObject.transform;
        }

        private Vector3 FollowingPoinPosition()
        {
            Vector3 followingPosition = _followingTarget.position;
            followingPosition.y += OffsetY;

            return followingPosition;
        }
    }
}
