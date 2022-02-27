using System;
using Tools.RX;

namespace Features.RewardsFeature
{
    public class PlayerRewardData
    {
        public SubscriptionProperty<DateTime?> LastTimeKeyDaily = new SubscriptionProperty<DateTime?>();
        public SubscriptionProperty<DateTime?> LastTimeKeyWeekly = new SubscriptionProperty<DateTime?>();

        public SubscriptionProperty<int> ActiveSlotKeyDaily = new SubscriptionProperty<int>();
        public SubscriptionProperty<int> ActiveSlotKeyWeekly = new SubscriptionProperty<int>();

        public SubscriptionProperty<int> WoodCount = new SubscriptionProperty<int>();
        public SubscriptionProperty<int> DiamondCount = new SubscriptionProperty<int>();

        private int _wood;
        private int _dimond;
    }
}