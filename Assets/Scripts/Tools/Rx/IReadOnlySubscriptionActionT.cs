using System;

namespace Tools
{
    public interface IReadOnlySubscriptionActionT<T>
    {
        void SubscribeOnChangeT(Action<T> subscriptionActionT);
        void UnSubscriptionOnChangeT(Action<T> unsubscriptionAction);
    }
}