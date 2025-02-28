using StaticData;
using UI.Services.Windows;

namespace Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowStaticData ForWindow(WindowId windowId);
        GateStaticData ForGate(GateTypeId gateKety);
        PlayerStaticData ForPlayer(PlayerTypeId playerTypeId);
    }
}