using System;

namespace Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string Level = "MainMenu";
        public string SavedLevel;
        public Vector3Data Position;
        private string initialLvl;


        public PositionOnLevel(string level, Vector3Data position)
        {
            SavedLevel = level;
            Position = position;
        }
        public PositionOnLevel(string initialLvl)
        {
            //Level = initialLvl;
            SavedLevel = initialLvl;
        }
    }
}