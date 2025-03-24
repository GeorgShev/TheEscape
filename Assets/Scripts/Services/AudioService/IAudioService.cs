using Audio;
using StaticData;

namespace Services.AudioService
{
    public interface IAudioService: IService
    {

        void Consturct(AudioCollection collection, IAudioPlayer audioPlayer);
        public void Initialize();
        public void StartMusic(AudioTypeId audioTypeId);
        public void PlaySound(AudioTypeId audioTypeId, float volume);
        public void PlaySound(AudioTypeId audioTypeId, bool isUI = false);
        public void SetMasterVolume(float volume);
        public void SetMusicVolume(float volume);
        public void SetSoundsVolume(float volume);
    }
}