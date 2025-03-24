using UnityEngine;

namespace Audio
{
    public interface IAudioPlayer
    {
        void Initialize();
        void StartMusic(AudioClip clip);
        void PlaySound(AudioClip clip,float volume);
        void PlaySound(AudioClip clip, bool isUI = false);
        void SetMasterVolume(float volume);
        void SetMusicVolume(float volume);
        void SetSoundsVolume(float volume);
        void Mute();
        void Unmute();
    }
}