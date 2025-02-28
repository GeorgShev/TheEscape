using Data;

namespace Services.PersistentProgressService
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}