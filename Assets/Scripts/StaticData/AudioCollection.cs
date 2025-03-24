using System.Linq;
using Audio;
using TriInspector;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "AudioCollection", menuName = "StaticData/AudioCollection")]
    public class AudioCollection : ScriptableObject
    {
        
        [ValidateInput(nameof(ValidateDublicates)), ListDrawerSettings(AlwaysExpanded = true)]
        [SerializeField] private NamedAudio[] _audio;

        public NamedAudio[] Audio => _audio;

        private TriValidationResult ValidateDublicates()
        {
            bool isUnique = _audio.GroupBy(x => x.audioType).All(grouping => grouping.Count() == 1);

            if (isUnique == true)
                return TriValidationResult.Valid;
            else
                return TriValidationResult.Warning("Duplicates found!");
        }
    }
}