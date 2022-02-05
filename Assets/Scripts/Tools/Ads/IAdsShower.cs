using System;

namespace Tools.Ads
{
    public interface IAdsShower
    {
        void ShowInterstitial();
        void ShowReward(Action successShow);
    }
}
