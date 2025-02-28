using System;
using Player;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        [FormerlySerializedAs("HeroState")] public State PlayerState;
        public Stats HeroStats;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            PlayerState = new State();
            HeroStats = new Stats();
        }

        
    }
}