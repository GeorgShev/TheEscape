
namespace Logic
{
    public interface IScore
    {
        int CurrentScore { get; set; }
        int HighestScore { get; set; }

        void TakePoints(int points);
    }
}
