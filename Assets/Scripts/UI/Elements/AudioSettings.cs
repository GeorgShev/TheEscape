using System;
using Services;
using Services.AudioService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class AudioSettings : MonoBehaviour
    {
        
        public Slider masterSlider;
        public Slider musicSlider;
        public Slider soundsSlider;
        
        private IAudioService _audioService;

        private void Start()
        {
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        public void ChangeMasterVolume()
        {
            _audioService.SetMasterVolume(masterSlider.value);
        }
        public void ChangeMusicVolume()
        {
            _audioService.SetMusicVolume(musicSlider.value);
        }
        public void ChangeSoundVolume()
        {
            _audioService.SetSoundsVolume(soundsSlider.value);
        }
    }
}
