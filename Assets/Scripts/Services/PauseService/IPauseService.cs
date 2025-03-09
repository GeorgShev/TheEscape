
namespace Services.PauseService
{
    public interface IPauseService : IService
    {
        bool IsPaused {get; set; }

        void SetPause(bool pause);
    }
}
