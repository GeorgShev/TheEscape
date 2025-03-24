using System;
using StaticData;
using TriInspector;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public struct NamedAudio
    {
        public AudioTypeId audioType;
        [Required] public AudioClip clip;
    }
}