using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        
        
        private const float DefaultMasterVolume = 0f;
        private const float MutedVolume = -80f;
        
        [Header("Components")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundsSource;
        [SerializeField] private AudioSource _uiSource;

        [Header("Mixer Groups")]
        [SerializeField] private AudioMixerGroup _masterGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private AudioMixerGroup _soundsGroup;
        
        public void Initialize()
        {
            _musicSource.loop = true;
            _soundsSource.loop = false;
        }
        
        public void StartMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void PlaySound(AudioClip clip, float volume)
        {
            _soundsSource.PlayOneShot(clip, volume);
        }

        public void PlaySound(AudioClip clip, bool isUI = false)
        {   
            if (isUI == true)
                _uiSource.PlayOneShot(clip);
            else
                _soundsSource.PlayOneShot(clip);
        }

        public void SetMasterVolume(float volume)
        {
            _masterGroup.audioMixer.SetFloat(_masterGroup.name, volume);
        }

        public void SetMusicVolume(float volume)
        {
            _musicGroup.audioMixer.SetFloat(_musicGroup.name, volume);
        }

        public void SetSoundsVolume(float volume)
        {
            _soundsGroup.audioMixer.SetFloat(_soundsGroup.name, volume);
        }

        public void Mute()
        {
            SetMasterVolume(MutedVolume);
        }

        public void Unmute()
        {
            SetMasterVolume(DefaultMasterVolume);
        }
    }
}
