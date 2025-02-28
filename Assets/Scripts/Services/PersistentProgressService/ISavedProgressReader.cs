using Data;

namespace Services.PersistentProgressService
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}