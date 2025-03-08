using Data;
using Services.PersistentProgressService;
using UnityEngine;

namespace Logic
{
    public class Score : MonoBehaviour, IScore, ISavedProgress
    {
        public int CurrentScore { get; set; }
        public int HighestScore { get; set; }
        
        
        public void TakePoints(int points)
        {
            CurrentScore += points;

            if (CurrentScore > HighestScore)
            {
                HighestScore = CurrentScore;
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            HighestScore = progress.ScoreData.highestScore;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (progress.ScoreData.highestScore > HighestScore)
            {
                progress.ScoreData.highestScore = HighestScore;
            }
        }
    }
}
