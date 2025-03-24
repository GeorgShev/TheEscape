using UnityEngine;
using UnityEngine.AddressableAssets;

namespace StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;

        
        public int Hp;
        public float Damage;

        public int MaxLoot;

        public int MinLoot;

        [Range(1f, 100f)]
        public float EffectiveDistane = 10f;

        [Range(1f, 100f)]
        public float Cleavage;

        [Range(1f, 100f)]
        public float MoveSpeed = 30f;


        public AssetReferenceGameObject PrefabEnemyReference;
    }
}
