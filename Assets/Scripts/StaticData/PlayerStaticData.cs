using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "StaticData/PlayerStats")]
    public class PlayerStaticData : ScriptableObject
    {

        public PlayerTypeId PlayerTypeIdTyoeId;
        
        public int CurrentHP;
        public int MaxHP;
        public float Damage;
        public float Speed;

        
    }
}
