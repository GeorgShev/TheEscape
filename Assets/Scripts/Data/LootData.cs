using System;

namespace Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public LootPieceDataDictionary LootPieceOnScene = new LootPieceDataDictionary();

        public Action ChangedValue;


        internal void Collect(Loot loot)
        {
            Collected += loot.Value;
            ChangedValue?.Invoke();
        }

        internal void Add(int loot)
        {
            Collected += loot;
            ChangedValue?.Invoke();
        }
    }
}