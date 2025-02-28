using Unity.Cinemachine;
using UnityEngine;

namespace CameraScripts
{
    class FollowPlayer : MonoBehaviour
    {
        public CinemachineCamera camera;


        public void FollowObject(GameObject followingObject)
        {
            camera.Follow = followingObject.transform;
        }
    }
}