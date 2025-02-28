using UnityEngine;

namespace Logic
{
    public class HurtBox : MonoBehaviour
    {
        public GameObject ColliderObject;
        public float Radius;


        private void Awake()
        {
            if (ColliderObject.GetComponent<SphereCollider>())
            {
                Radius = ColliderObject.GetComponent<SphereCollider>().radius;
            }
            else if (ColliderObject.GetComponent<BoxCollider>()) 
            {
                float x = (ColliderObject.GetComponent<BoxCollider>().size.x);
                float y = (ColliderObject.GetComponent<BoxCollider>().size.y);
                float z = (ColliderObject.GetComponent<BoxCollider>().size.z);
                Radius = ((x < y) ? (x < z ? x : z) : (y < z ? y : z)) /2;
            }
            
        }
    }
}
