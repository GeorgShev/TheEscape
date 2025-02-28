using UI.Services.Windows;
using UI.Windows;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/Window static data")]
    public class WindowStaticData : ScriptableObject
    {
        //public List<WindowConfig> Configs;

        public WindowId WindowId;
        public WindowBase Prefab;
    }
}
