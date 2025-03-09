namespace Services.PauseService
{
    public class PauseService : IPauseService
    {
        public bool IsPaused { get; set; }

        public void SetPause(bool pause)
        {
            IsPaused = pause;
        }
    }
}
