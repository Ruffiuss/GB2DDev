using System;

namespace Tools.RX
{
    internal interface IReadOnlySubscriptionAction
    {
        void SubscribeOnChange(Action subscriptionAction);
        void UnSubscriptionOnChange(Action unsubscriptionAction);
    }
}