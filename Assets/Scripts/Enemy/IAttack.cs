using UnityEngine;

namespace Enemy
{
    public interface IAttack
    {

        void Construct(Transform heroTransform, float damage);
        void EnableAttack();
        void DisableAttack();
    }
}