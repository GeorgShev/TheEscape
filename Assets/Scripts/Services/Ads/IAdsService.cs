﻿using System;

namespace Services.Ads
{
    public interface IAdsService : IService
    {
        event Action RewardedVideoReady;
        bool IsRewardedVideoReady { get; }
        int Reward { get; }
        void Initialize();
        void ShowRewardedVideo(Action OnVideoFinished);
    }
}