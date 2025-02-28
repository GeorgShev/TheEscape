using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using StaticData;
using UI.Services.Windows;

namespace Services.StaticDataService
{

    public class StaticDataService : IStaticDataService
    {
        private const string _staticDataLevelPath = "StaticData/Levels";
        private const string _staticDataSceneAssetsPath = "StaticData/SceneAssets";
        private const string _staticDataenemyPath = "StaticData/Enemy";
        private const string _staticDataWindowPath = "StaticData/UI";
        private const string _staticDataHeroPath = "StaticData/Player";
        
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemy;
        private Dictionary<GateTypeId, GateStaticData> _gate;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowStaticData> _windowConfigs;
        private Dictionary<PlayerTypeId, PlayerStaticData> _playerConfigs;
   

        public void Load()
        {
            _enemy = Resources.LoadAll<EnemyStaticData>(_staticDataenemyPath).ToDictionary(x => x.EnemyTypeId, x => x);

            _gate = Resources.LoadAll<GateStaticData>(_staticDataSceneAssetsPath).ToDictionary(x => x.GateTypeId, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(_staticDataLevelPath).ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources.LoadAll<WindowStaticData>(_staticDataWindowPath).ToDictionary(x => x.WindowId, x => x);

            _playerConfigs = Resources.LoadAll<PlayerStaticData>(_staticDataHeroPath).ToDictionary(x => x.PlayerTypeIdTyoeId, x => x);

        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId)
        {
            if (_enemy.TryGetValue(typeId, out EnemyStaticData monsterStaticData))
            {
                return monsterStaticData;
            }
            return null;
        }

        public GateStaticData ForGate(GateTypeId typeId)
        {
            if (_gate.TryGetValue(typeId, out GateStaticData gateStaticData))
            {
                return gateStaticData;
            }
            return null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData))
            {
                return levelStaticData;
            }
            return null;
        }


        public WindowStaticData ForWindow(WindowId windowId)
        {
            if (_windowConfigs.TryGetValue(windowId, out WindowStaticData windowStaticData))
            {
                return windowStaticData;
            }
            return null;
        }
        

        public PlayerStaticData ForPlayer(PlayerTypeId playerTypeId)
        {
            if (_playerConfigs.TryGetValue(playerTypeId, out PlayerStaticData playerStaticData))
            {
                return playerStaticData;
            }
            return null;
        }
        
    }
}
