using System.Collections.Generic;
using Data;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public bool MenuLevel;

        public List<EnemySpawnerStaticData> EnemySpawnerData;

        public Vector3 InitialHeroPosition;
        public LevelTransferStaticData LevelTransfer;
        public GateSpawnerData LevelGate;
    }
}
