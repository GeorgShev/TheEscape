using System.Collections.Generic;
using System.Linq;
using Audio;
using StaticData;
using UnityEngine;

namespace Services.AudioService
{
    public class AudioService : IAudioService
    {

        
         private AudioCollection _collection;
        
        private IAudioPlayer _audioPlayer;

        private Dictionary<AudioTypeId, AudioClip> _clips;

        public void Consturct(AudioCollection collection, IAudioPlayer audioPlayer)
        {
            _collection = collection;
            _audioPlayer = audioPlayer;
        }
        public void Initialize()
        {
            _clips = _collection.Audio.ToDictionary(audio => audio.audioType, audio => audio.clip);
            _audioPlayer.Initialize();
        }

        public void StartMusic(AudioTypeId audioTypeId)
        {
            _audioPlayer.StartMusic(_clips[audioTypeId]);
        }

        public void PlaySound(AudioTypeId audioTypeId, float volume)
        {
            _audioPlayer.PlaySound(_clips[audioTypeId], volume);
        }

        public void PlaySound(AudioTypeId audioTypeId, bool isUI = false)
        {   
            _audioPlayer.PlaySound(_clips[audioTypeId], isUI);
        }

        public void SetMasterVolume(float volume)
        {
            _audioPlayer.SetMasterVolume(volume);
        }

        public void SetMusicVolume(float volume)
        {
            _audioPlayer.SetMusicVolume(volume);
        }

        public void SetSoundsVolume(float volume)
        {
            _audioPlayer.SetSoundsVolume(volume);
        }

        public void Mute()
        {
            _audioPlayer.Mute();
        }

        public void Unmute()
        {
            _audioPlayer.Unmute();
        }
    }
}